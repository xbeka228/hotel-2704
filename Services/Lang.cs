using CommunityToolkit.Mvvm.ComponentModel;

namespace HotelManagement.Services;

public partial class Lang : ObservableObject
{
    public static Lang Instance { get; } = new();

    private bool _isKazakh;
    public bool IsKazakh => _isKazakh;
    public string CurrentLang => _isKazakh ? "KZ" : "RU";

    public void Toggle()
    {
        _isKazakh = !_isKazakh;
        // Notify ALL properties changed
        OnPropertyChanged(string.Empty);
    }

    // ===== GENERAL =====
    public string AppTitle => _isKazakh ? "ГРАНД ҚОНАҚ ҮЙ" : "ГРАНД ОТЕЛЬ";
    public string SystemTitle => _isKazakh ? "Басқару жүйесі" : "Система управления";
    public string Navigation => _isKazakh ? "НАВИГАЦИЯ" : "НАВИГАЦИЯ";
    public string Role => _isKazakh ? "Рөл" : "Роль";
    public string Logout => _isKazakh ? "Шығу" : "Выйти";
    public string Back => _isKazakh ? "← Артқа" : "← Назад";
    public string Save => _isKazakh ? "Сақтау" : "Сохранить";
    public string Cancel => _isKazakh ? "Болдырмау" : "Отмена";
    public string Delete => _isKazakh ? "Жою" : "Удалить";
    public string Refresh => _isKazakh ? "Жаңарту" : "Обновить";
    public string Language => _isKazakh ? "Тіл: KZ" : "Язык: RU";
    public string SwitchLang => _isKazakh ? "🌐 Орысша" : "🌐 Қазақша";
    public string Currency => "тг";

    // ===== ROLES =====
    public string Guest => _isKazakh ? "Қонақ" : "Гость";
    public string Staff => _isKazakh ? "Қызметкер" : "Персонал";
    public string Admin => _isKazakh ? "Әкімші" : "Администратор";
    public string GuestBtn => _isKazakh ? "🏠  Қонақ" : "🏠  Гость";
    public string StaffBtn => _isKazakh ? "👤  Қызметкер" : "👤  Персонал";
    public string AdminBtn => _isKazakh ? "⚙  Әкімші" : "⚙  Администратор";

    // ===== LOGIN =====
    public string Authorization => _isKazakh ? "Авторизация" : "Авторизация";
    public string LoginAs => _isKazakh ? "Кіру: " : "Вход как: ";
    public string LoginLabel => _isKazakh ? "Логин" : "Логин";
    public string PasswordLabel => _isKazakh ? "Құпия сөз" : "Пароль";
    public string EnterLogin => _isKazakh ? "Логинді енгізіңіз" : "Введите логин";
    public string EnterPassword => _isKazakh ? "Құпия сөзді енгізіңіз" : "Введите пароль";
    public string LoginButton => _isKazakh ? "Кіру" : "Войти";
    public string LoginError => _isKazakh ? "Логин немесе құпия сөз қате!" : "Неверный логин или пароль!";
    public string LoginRequired => _isKazakh ? "Логин мен құпия сөзді енгізіңіз!" : "Введите логин и пароль!";

    // ===== GUEST / ROOMS =====
    public string SelectRoom => _isKazakh ? "Бөлмені таңдаңыз" : "Выберите номер";
    public string CurrentClass => _isKazakh ? "Ағымдағы класс" : "Текущий класс";
    public string RoomNumber => _isKazakh ? "Бөлме" : "Номер";
    public string PricePerDay => _isKazakh ? "тг/тәу" : "тг/сут";
    public string Available => _isKazakh ? "Бос" : "Свободен";
    public string Occupied => _isKazakh ? "Бос емес" : "Занят";
    public string BackToRooms => _isKazakh ? "← Бөлмелерге оралу" : "← Назад к номерам";
    public string BookRoom => _isKazakh ? "Брондау" : "Забронировать";
    public string RoomPhotos => _isKazakh ? "Бөлме фотосуреттері:" : "Фото номера:";
    public string PriceLabel => _isKazakh ? "тг / тәулік" : "тг / сутки";

    // ===== BOOKING FORM =====
    public string Booking => _isKazakh ? "Брондау" : "Бронирование";
    public string GuestName => _isKazakh ? "Қонақтың аты-жөні" : "ФИО гостя";
    public string GuestNamePlaceholder => _isKazakh ? "Иванов Иван Иванович" : "Иванов Иван Иванович";
    public string PhoneLabel => _isKazakh ? "Байланыс телефоны" : "Телефон для связи";
    public string PhonePlaceholder => "+7 (777) 123-45-67";
    public string SubmitBooking => _isKazakh ? "Өтінімді жіберу" : "Отправить заявку";
    public string BookingSuccess => _isKazakh
        ? "Брондау өтінімі жіберілді! Қызметкерлер сізбен хабарласады."
        : "Заявка на бронирование отправлена! Персонал свяжется с вами.";
    public string EnterName => _isKazakh ? "Аты-жөнін енгізіңіз!" : "Введите ФИО!";
    public string PhoneDigitsError => _isKazakh
        ? "Телефон нөмірінде 11 сан болуы керек! (+7XXXXXXXXXX)"
        : "Номер телефона должен содержать 11 цифр! (+7XXXXXXXXXX)";
    public string EnterPhone => _isKazakh ? "Телефон нөмірін енгізіңіз!" : "Введите номер телефона!";

    // ===== STAFF =====
    public string ManageBookings => _isKazakh ? "Брондауларды басқару" : "Управление бронированиями";
    public string Pending => _isKazakh ? "Күтуде" : "Ожидающие";
    public string Confirmed => _isKazakh ? "Расталған" : "Подтверждённые";
    public string All => _isKazakh ? "Барлығы" : "Все";
    public string ConfirmBtn => _isKazakh ? "✓ Растау" : "✓ Подтвердить";
    public string RejectBtn => _isKazakh ? "✗ Қабылдамау" : "✗ Отклонить";
    public string ReceiptBtn => _isKazakh ? "Чек PDF" : "Чек PDF";
    public string ReceiptTitle => _isKazakh ? "Чек беру (QuestPDF)" : "Выдача чека (QuestPDF)";
    public string DaysCount => _isKazakh ? "Тәулік саны:" : "Количество суток:";
    public string GeneratePdf => _isKazakh ? "PDF жасау" : "Сформировать PDF";
    public string GuestLabel => _isKazakh ? "Қонақ:" : "Гость:";
    public string PhoneShort => _isKazakh ? "Тел:" : "Тел:";

    public string BookingConfirmed(int id) => _isKazakh
        ? $"№{id} брондау расталды!" : $"Бронирование №{id} подтверждено!";
    public string BookingRejected(int id) => _isKazakh
        ? $"№{id} брондау қабылданбады." : $"Бронирование №{id} отклонено.";
    public string ConfirmFailed => _isKazakh
        ? "Брондауды растау мүмкін болмады." : "Не удалось подтвердить бронирование.";
    public string RejectFailed => _isKazakh
        ? "Брондауды қабылдамау мүмкін болмады." : "Не удалось отклонить бронирование.";
    public string EnterDays => _isKazakh
        ? "Тәулік санын көрсетіңіз!" : "Укажите количество суток!";
    public string ReceiptSaved(string path) => _isKazakh
        ? $"Чек сақталды: {path}" : $"Чек сохранён: {path}";

    // ===== ADMIN =====
    public string ManageRooms => _isKazakh ? "Бөлмелерді басқару" : "Управление номерами";
    public string AddRoom => _isKazakh ? "+ Бөлме қосу" : "+ Добавить номер";
    public string EditBtn => _isKazakh ? "Өзгерту" : "Редакт.";
    public string StatusBtn => _isKazakh ? "Мәртебе" : "Статус";
    public string AddRoomTitle => _isKazakh ? "Жаңа бөлме қосу" : "Добавление нового номера";
    public string EditRoomTitle(int num) => _isKazakh
        ? $"{num} бөлмені өзгерту" : $"Редактирование номера {num}";
    public string RoomNumberLabel => _isKazakh ? "Бөлме нөмірі" : "Номер комнаты";
    public string ClassLabel => _isKazakh ? "Класс" : "Класс";
    public string PricePerDayLabel => _isKazakh ? "Тәулік бағасы (тг)" : "Цена за сутки (тг)";
    public string DescriptionLabel => _isKazakh ? "Сипаттама" : "Описание";
    public string DescriptionPlaceholder => _isKazakh ? "Бөлме сипаттамасы..." : "Описание номера...";
    public string Photo => _isKazakh ? "Фото" : "Фото";
    public string InvalidRoomNumber => _isKazakh
        ? "Дұрыс бөлме нөмірін енгізіңіз!" : "Введите корректный номер комнаты!";
    public string InvalidPrice => _isKazakh
        ? "Дұрыс бағаны енгізіңіз!" : "Введите корректную цену!";
    public string RoomDeleted(int num) => _isKazakh
        ? $"{num} бөлме жойылды." : $"Номер {num} удалён.";
    public string RoomStatusChanged(int num, bool wasAvailable) => _isKazakh
        ? $"{num} бөлме — {(wasAvailable ? "Бос емес" : "Бос")}"
        : $"Номер {num} — {(wasAvailable ? "Занят" : "Свободен")}";

    // ===== RECEIPT PDF =====
    public string ReceiptPdfTitle => _isKazakh ? "ҚОНАҚ ҮЙ «ГРАНД ҚОНАҚ ҮЙ»" : "ГОСТИНИЦА «ГРАНД ОТЕЛЬ»";
    public string ReceiptPdfSubtitle => _isKazakh ? "ТӨЛЕМ ЧЕГІ" : "ЧЕК ОБ ОПЛАТЕ";
    public string ReceiptDate => _isKazakh ? "Күні:" : "Дата:";
    public string ReceiptBookingNo => _isKazakh ? "Брондау №:" : "Бронирование №:";
    public string ReceiptGuestInfo => _isKazakh ? "Қонақ туралы ақпарат:" : "Информация о госте:";
    public string ReceiptFio => _isKazakh ? "Аты-жөні:" : "ФИО:";
    public string ReceiptPhone => _isKazakh ? "Телефон:" : "Телефон:";
    public string ReceiptRoomInfo => _isKazakh ? "Бөлме туралы ақпарат:" : "Информация о номере:";
    public string ReceiptRoomNumber => _isKazakh ? "Бөлме нөмірі:" : "Номер комнаты:";
    public string ReceiptClass => _isKazakh ? "Класс:" : "Класс:";
    public string ReceiptCalc => _isKazakh ? "Құн есебі:" : "Расчёт стоимости:";
    public string ReceiptPricePerDay => _isKazakh ? "Тәулік бағасы:" : "Цена за сутки:";
    public string ReceiptDaysCount => _isKazakh ? "Тәулік саны:" : "Количество суток:";
    public string ReceiptTotal => _isKazakh ? "БАРЛЫҒЫ:" : "ИТОГО:";
    public string ReceiptThanks => _isKazakh
        ? "Біздің қонақ үйді таңдағаныңыз үшін рахмет!"
        : "Спасибо за выбор нашего отеля!";
    public string ReceiptContacts => "Тел: +7 (7172) 12-34-56 | www.grandhotel.kz";

    // ===== STATUS CONVERTER =====
    public string StatusText(bool isAvailable) => isAvailable
        ? (_isKazakh ? "Бос" : "Свободен")
        : (_isKazakh ? "Бос емес" : "Занят");
}
