# Тестовое задание для Algimed
Выполнил: Клишевич Артем<br>

<hr>
<h2>Документация и навигация для проверяющего</h2>
Инсталляционный пакет <b>Algimed.msi</b> находится в директории <b>Algimed.Setup/version</b>.<br><br>

В качестве локальной БД была использована <b>SQL lite</b>. В базе данных находится 2 пользовательские таблицы : Users (содержит информацию о пользователях) и Results (содержит результат математической операции пересечения ячеек).
<br>Все модели находятся в директории <b>Algimed/Models</b>.<br>
Модель пользователя <b>User</b> имеет 4 поля : Id, Email, Name (Not required), Password. <br>
Модель результата <b>Result</b> имеет 2 поля : Cells, Value<br><br>
Попасть на основную страницу можно только после прохождения авторизации. <br>
На основной странице выводятся данные из таблицы <b>Results</b>, загрузить в которую их можно с помощью кнопки <b>Load file</b>. Загрузить можно только <b>.csv</b> и <b>.xlsx</b> файлы. 


<h2>Работа парсера</h2>
Вся логика находится в файле <b>FileHelperService.cs</b> директории <b>Algimed/Services</b>.<br><br>
Парсер ищет в загруженном файле 6 ячеек, которые в 1 колонке содержат одну из строк "C01", "D01", "F01", "E01", "G01", "H01", сохраняя значения из колонки 6 этой строки в словарь : Key = 1 cell; Value = 6 cell.<br>
"C01", "D01", "F01" сохраняются в словарь <b>cols</b>,
"E01", "G01", "H01" сохраняются в словарь <b>rows</b>.<br> 
Далее перебирая все взаимодействия элементов первого словаря со вторым по формуле <b>(1)</b> 
данные сохраняются в таблицу <b>Results</b> базы данных.

```commandline
(1)   Result.Value = (col.Value - row.Value)^2.
(2)   Result.Cells = $"{col.Key}_{row.Key}"
```

<h2>Особенности работы SQL lite</h2>
База данных SQL lite может в работать в режиме read only, если папка в которой находится база данных не имеет прав на запись. Из-за этого могут возникать ошибки с операциями записи в базу данных. Чтобы дать доступ на запись :<br>
- ПКМ по папке, где находится файл базы данных <b>algimed.db</b><br>- Свойства <br>- Безопасность <br>- Изменить <br>- Выбираем строку <b>пользователи</b> и ставим галочку <b>разрешить</b> в строке <b>Полный доступ</b> 
