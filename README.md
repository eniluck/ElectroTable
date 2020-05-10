
## Реализация тестовой задачки

Необходимо реализовать простейшую версию электронной таблицы на языке программирования C#.

На вход подается двумерный массив выражений, разделенных запятыми или табуляциями. В качестве элементов массива могут выступать числа или выражения, в которых допускаются операции:
*	сложения;
*	вычитания;
*	умножения;
*	ссылка на другие элементы массива;
*	скобки.

**Пример:** R1C3 + 2*(R8C9 -  R10C1), где R1C3 ссылка на элемент в первой строке третьего столбца входного массива.

Необходимо реализовать консольное приложение, которое будет на выходе записывать в текстовый файл двумерный массив с вычисленными выражениями (возможные разделители: запятые или табуляции) или сообщать об ошибках при выходе за пределы таблицы или при наличии циклических ссылок в stderror.
