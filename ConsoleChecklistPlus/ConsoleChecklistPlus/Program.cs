using ConsoleChecklistPlus;
using System.Text.Json;

string version = "1.1.0R";
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"ConsoleChecklist Plus - Version: {version}");
Console.ForegroundColor = ConsoleColor.DarkGray;
Console.WriteLine("Загрузка...");

int selectedTask = 0;
int pinnedTask = 0;
string selectedTaskPre;
string newTaskName;
string newTaskDescription;
int newTaskPriority = 0;
string answer;
string jsonString;
List<CCPTask> TaskN = new List<CCPTask>();

if (File.Exists("CCPSave.txt") == true)
{
    string fileName = "CCPSave.txt";
    jsonString = File.ReadAllText(fileName);
    TaskN = JsonSerializer.Deserialize<List<CCPTask>>(jsonString)!;
    if (TaskN.Count == 0)
    {
        Console.WriteLine("Файл с сохранением найден, но он ничего не содержит (сохранение не загружено)");
    }
    else
    {
        Console.WriteLine("Файл с сохранением найден, сохранение загружено");
    }
}
else 
{
    Console.WriteLine("Файл сохранения не найден");
}

void Menu() //Меню
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╚ Console Checklist Plus | Меню");
    Console.WriteLine();

    if (pinnedTask != 0)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("----------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Закреплено - {TaskN[pinnedTask].Name}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(TaskN[pinnedTask].Description);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Приоритет: {TaskN[pinnedTask].Priority}");
        if (TaskN[pinnedTask].State == true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Задача выполнена!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задача не выполнена.");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine();
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("═ Выберите действие:");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("» 1 - Создать новую задачу");
    if (TaskN.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("» 2 - Посмотреть все задачи (Сначала создайте задачи)");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("» 2 - Посмотреть все задачи");
    }
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("» 3 - Сохранить");
    Console.WriteLine("» 4 - Удалить все");
    Console.WriteLine("» 5 - О приложении");
    Console.WriteLine();
    MenuSA();
}

void MenuSA() //Выбор действия в меню
{
    Console.ForegroundColor = ConsoleColor.White;
    answer = Console.ReadLine();
    switch (answer)
    {
        case "1":
            TaskCreate();
            break;
        case "2":
            ViewTasks();
            break;
        case "3":
            Save();
            MenuSA();
            break;
        case "4":
            Console.WriteLine("Вы уверенны, что хотите удалить все данные? (Введите 'yes' для удаления)");
            answer = Console.ReadLine();
            if (answer == "yes")
            {
                File.WriteAllText("CCPSave.txt", "[]");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Все данные удалены");
                Console.ReadLine();
                Menu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Данные не были удалены");
                Console.ReadLine();
                Menu();
            }
            break;
        case "5":
            Console.Clear();
            Console.WriteLine("Console Ckecklist Plus");
            Console.WriteLine($"Version: {version}");
            Console.ReadLine();
            Menu();
            break;
        default:
            MenuSA();
            break;
    }
}

void TaskCreate() //Создание задач
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╚ Создание задачи - введите название для задачи ╜");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("-------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write("Название: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    newTaskName = Console.ReadLine();

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"╚ {newTaskName} - Введите описание для задачи");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("-------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write("Описание: ");
    Console.ForegroundColor = ConsoleColor.White;
    newTaskDescription = Console.ReadLine();

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"╚ {newTaskName} - Дополнительные настройки");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Задайте приоритет для задачи (введите любое число)");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("-------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Приоритет: ");
    Console.ForegroundColor = ConsoleColor.Magenta;

    for (int parseA = 0; parseA < 1;)
    {
        if (int.TryParse(Console.ReadLine(), out newTaskPriority))
        {
            parseA = 1;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Неверное значение! Введите число");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Приоритет: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            parseA = 0;
        }
    }

    TaskN.Add(new CCPTask(TaskN.Count, newTaskName, newTaskDescription, false, newTaskPriority));

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine();
    Console.WriteLine($"Задача [{TaskN[TaskN.Count-1].Name}] успешно создана!");
    Console.ReadLine();
    Menu();
}

void ViewTasks() //Просмотр всех задач
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╚ Ваши задачи:");
    Console.WriteLine();

    if (TaskN.Count == 0)
    { Menu(); }

    for (int i = 0; i < TaskN.Count; i++)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("----------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Задача #{i + 1} - {TaskN[i].Name}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(TaskN[i].Description);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Приоритет: {TaskN[i].Priority}");
        if (TaskN[i].State == true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Задача выполнена!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задача не выполнена.");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine();
    }
    TaskSelect();
}

void TaskSelect() //Выбор задачи
{
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("Выберите задачу (x - выйти)");

    Console.ForegroundColor = ConsoleColor.White;

    for (int parseA = 0; parseA < 1;)
    {
        selectedTaskPre = Console.ReadLine();
        if (selectedTaskPre == "x")
        {
            Console.Clear();
            Menu();
            break;
        }
        if (int.TryParse(selectedTaskPre, out selectedTask))
        {
            parseA = 1;
            selectedTask--;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Неверное значение! Введите число");
            Console.ForegroundColor = ConsoleColor.White;
            parseA = 0;
        }
        
    }

    if (selectedTask > TaskN.Count)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Задача не найдена");
        TaskSelect(); 
    }
    if (selectedTask < 1)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Задача не найдена");
        TaskSelect(); 
    }
    else
    {
        TaskAct(); 
    }
}

void TaskAct() //Действие с задачей
{
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("Что сделать с этой задачей? (x - выйти)");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("1 - Изменить состояние (Выполнено/не выполнено)");
    Console.WriteLine("2 - Изменить задачу");
    Console.WriteLine("3 - Удалить задачу");
    Console.WriteLine("4 - Закрепить");

    Console.ForegroundColor = ConsoleColor.White;
    answer = Console.ReadLine();
    switch (answer)
    {
        case "1": //Изменить состояние
            if (TaskN[selectedTask].State == true)
            { TaskN[selectedTask].State = false; }
            else
            { TaskN[selectedTask].State = true; }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Успешно!");
            Console.ReadLine();
            ViewTasks();
                break;
        case "2": //Изменить задачу
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╚ Изменить задачу {TaskN[selectedTask].Name}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Введите новое имя для задачи: (если не надо изменять то введите 'skip')");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("-------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Новое имя: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            answer = Console.ReadLine();
            if (answer == "skip")
            { }
            else
            { TaskN[selectedTask].Name = answer; }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Введите новое описание для задачи: (если не надо изменять то введите 'skip')");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("-------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Новое описание: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            answer = Console.ReadLine();
            if (answer == "skip")
            { }
            else
            { TaskN[selectedTask].Description = answer; }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Изменить приоритет для задачи:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("-------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Приоритет: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            answer = Console.ReadLine();
            TaskN[selectedTask].Priority = int.Parse(answer);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Задача успешно изменена!");
            Console.ReadLine();
            ViewTasks();
            break;
        case "3": //Удалить задачу
            TaskN.RemoveAt(selectedTask);
            ViewTasks();
            break;
        case "4":
            pinnedTask = selectedTask;
            Console.WriteLine("Задача закреплена!");
            Console.ReadLine();
            ViewTasks();
            //test
            break;
        case "x": //Выйти
            ViewTasks();
            break;
        default:
            TaskAct();
            break;
    }
}

void Save()
{
    jsonString = JsonSerializer.Serialize(TaskN);
    File.WriteAllText("CCPSave.txt", jsonString);
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("Сохранено");
}

Console.ForegroundColor = ConsoleColor.Green;
Console.Write("Проект успешно загружен!");
Console.ForegroundColor = ConsoleColor.White;
Console.Write(" (Нажмите [Enter])");
Console.ReadLine();
Menu();