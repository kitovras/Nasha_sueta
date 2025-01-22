namespace OurFuss.TelegramBot.EventGeneration.Parameters;

public static class MessageParameters
{
    internal static string DeferredPublicationMessage = "Выберете через сколько дней выполнить публикацию (относительно текущей даты и времени):";

    internal static string TechnicalErrorMessage = "Произошла ошибка. Отправьте команду /support для получения инструкций.";

    internal static string UnknownCommandMessage = "Неизвестная команда. Отправьте <a>/start</a> для регистрации в системе и получения инструкций.";

    internal static string Welcome =
        "Для публикации своего мероприятия укажите:\n" +
        "1. Название мероприятия\n" +
        "2. Описание мероприятия\n" +
        "3. Адрес проведения\n" +
        "4. Дата и время\n" +
        "5. Телефон, ссылка для уточнения\n" +
        "6. Цена\n" +
        "7. Возрастное ограничение\n" +
        "8. Изображение для афиши\n\n" +
        "Имейте ввиду, что перед публикацией все мероприятия проходят модерацию!";
}
