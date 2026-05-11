using HotelManagement.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HotelManagement.Services;

public static class ReceiptService
{
    public static string GenerateReceipt(Booking booking, int days)
    {
        var l = Lang.Instance;
        var total = booking.RoomPrice * days;
        var filePath = Path.Combine(Path.GetTempPath(), $"receipt_{booking.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5);
                page.MarginHorizontal(40);
                page.MarginVertical(30);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text(l.ReceiptPdfTitle)
                        .Bold().FontSize(20).FontColor(Colors.Blue.Darken3);
                    col.Item().AlignCenter().Text(l.ReceiptPdfSubtitle)
                        .SemiBold().FontSize(14).FontColor(Colors.Grey.Darken2);
                    col.Item().PaddingVertical(5).LineHorizontal(2).LineColor(Colors.Blue.Darken3);
                });

                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Spacing(6);

                    col.Item().Text($"{l.ReceiptDate} {DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(11);
                    col.Item().Text($"{l.ReceiptBookingNo} {booking.Id}").FontSize(11);

                    col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    col.Item().Text(l.ReceiptGuestInfo).Bold().FontSize(12);
                    col.Item().PaddingLeft(15).Text($"{l.ReceiptFio} {booking.GuestName}");
                    col.Item().PaddingLeft(15).Text($"{l.ReceiptPhone} {booking.GuestPhone}");

                    col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    col.Item().Text(l.ReceiptRoomInfo).Bold().FontSize(12);
                    col.Item().PaddingLeft(15).Text($"{l.ReceiptRoomNumber} {booking.RoomNumber}");
                    col.Item().PaddingLeft(15).Text($"{l.ReceiptClass} {booking.RoomClass}");

                    col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    col.Item().Text(l.ReceiptCalc).Bold().FontSize(12);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                        });

                        table.Cell().PaddingLeft(15).Text(l.ReceiptPricePerDay);
                        table.Cell().AlignRight().Text($"{booking.RoomPrice:N0} тг");

                        table.Cell().PaddingLeft(15).Text(l.ReceiptDaysCount);
                        table.Cell().AlignRight().Text($"{days}");

                        table.Cell().PaddingLeft(15).PaddingTop(5).Text("").FontSize(1);
                        table.Cell().PaddingTop(5).Text("").FontSize(1);
                    });

                    col.Item().PaddingVertical(5).LineHorizontal(2).LineColor(Colors.Blue.Darken3);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                        });
                        table.Cell().PaddingLeft(15).Text(l.ReceiptTotal).Bold().FontSize(16);
                        table.Cell().AlignRight().Text($"{total:N0} тг").Bold().FontSize(16)
                            .FontColor(Colors.Blue.Darken3);
                    });
                });

                page.Footer().Column(col =>
                {
                    col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                    col.Item().AlignCenter().Text(l.ReceiptThanks)
                        .Italic().FontSize(11).FontColor(Colors.Grey.Darken1);
                    col.Item().AlignCenter().Text(l.ReceiptContacts)
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        }).GeneratePdf(filePath);

        return filePath;
    }
}
