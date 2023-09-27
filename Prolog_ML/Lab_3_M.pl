
start:-
	repeat,
	main_menu,
	readln(A),
	check(A),
	!.

fam :-
repeat,
fam_menu,
readln(A),
fam_check(A),
!.

func :-
repeat,
dop_func_menu,
readln(B),
func_check(B),
!.

dop_func_menu :-
writeln("\nВыберите с каким числом производить вычисления:"),
writef("1 - Работа с положительным числом\n2 - Работа с отрицательным числом\n3 - Завершение работы\n").

main_menu :-
writeln("\nВыберите комманду из списка:"),
writef("1 - Работа с семейным древом\n2 - Вычисление функции arctg(x)\n3 - Завершение работы программы\n").

fam_menu :-
writeln("\nВыберите комманду из списка:"),
writef("1 - Является ли Таня дочерью Александра?\n2 - Является ли Надя бабушкой?\n3 - Кто является внуком Петра?\n4 - У кого д/р весной?\n5 - Выход\n").

func_menu :-
writeln("\nВыберите комманду из списка:"),
writef("1 - Прямое вычисление функции\n2 - Вычисление функции по приближенной формуле \n3 - Сравнение реального значения и приближенного\n4 - Выход\n").

func_check([Command]) :-
Command = 1 ->( func_menu,
readln(A),
func_check_pos(A), fail
);
Command = 2 ->( func_menu,
readln(A),
func_check_neg(A), fail
);
Command = 3 -> writeln("Выход"), !.

check([Command]) :-
Command = 1 -> (fam, fail);
Command = 2 -> (func, fail);
Command = 3 -> writeln("Программа завершена").

fam_check([Command]) :-
Command = 1 -> (goal1, fail);
Command = 2 -> (goal2, fail);
Command = 3 -> (goal3, fail);
Command = 4 -> (birthday_in_spring(Person), fail);
Command = 5 -> writeln("Выход"), !.


func_check_pos([Command]) :-
Command = 1 -> (
writeln("Введите X:"), readln(ListX), ListX = [X],
func(X, Res),
format("X = ~w. Функция = ~w", [X, Res]),
fail
);
Command = 2 -> (
writeln("Введите X:"), readln(ListX), ListX = [X],
writeln("Введите N:"), readln(ListN), ListN = [N],
step(X, N, Res),
format("X = ~w and N = ~w. Функция = ~w", [X, N, Res]),
fail
);
Command = 3 -> (
writeln("Введите X:"), readln(ListX), ListX = [X],
writeln("Введите N:"), readln(ListN), ListN = [N],
test(X, N),
fail
);
Command = 4 -> writeln("Выход"), !.


func_check_neg([Command]) :-
Command = 1 -> (
writeln("Введите модуль X:"), readln(ListX), ListX = [X],
X_1 is X * -1,
func(X_1, Res), 
format("X = ~w. Функция = ~w", [X, Res]),
fail
);
Command = 2 -> (
writeln("Введите модуль X:"), readln(ListX), ListX = [X],
writeln("Введите N:"), readln(ListN), ListN = [N],
X_1 is X * -1,
step(X_1, N, Res), 
format("X = ~w and N = ~w. Функция = ~w", [X_1, N, Res]),
fail
);
Command = 3 -> (
writeln("Введите модуль X:"), readln(ListX), ListX = [X],
writeln("Введите N:"), readln(ListN), ListN = [N],
X_1 is X * -1,
test((X_1), N),
fail
);
Command = 4 -> writeln("Выйти из режима работы с функцией"), !.






%1 лаба% задание простого двуместного предиката (фактов) "родитель"

parent(petr,larisa).
parent(petr,marina).
parent(nadya,larisa).
parent(nadya,marina).
parent(larisa,sergey).
parent(larisa,ivan).
parent(alexey,sergey).
parent(alexey,ivan).
parent(marina,egor).
parent(marina,tanya).
parent(alexander,egor).
parent(alexander,tanya).

% задание простого одноместного предиката (фактов) "мужчина"

male(petr).
male(alexey).
male(sergey).
male(ivan).
male(alexander).
male(egor).

% задание простого одноместного предиката (фактов) "женщина"

female(nadya).
female(larisa).
female(marina).
female(tanya).


% задание простого двуместного предиката (фактов) "место рождения"

born(petr,kazyli).
born(nadya,chukaevo).
born(alexey,kazan).
born(larisa,rubnyaSloboda).
born(marina,rubnyaSloboda).
born(alexander,rubnyaSloboda).
born(sergey,rubnyaSloboda).
born(ivan,kazan).
born(tanya,kazan).
born(egor,kazan).


% задание простого 4-местного предиката "дата рождения"

birthday(petr,17,april,1957).
birthday(nadya,16, august,1960).
birthday(alexey,10,mai,1976).
birthday(larisa,1,august,1980).
birthday(marina,20,april,1982).
birthday(alexander,30,january,1980).
birthday(sergey,16,april,2003).
birthday(ivan,24,march,2008).
birthday(tanya,15,february,2009).
birthday(egor,29,julie,2013).

%задание сложного предиката (правила) "отец"

father(X,Y) :- parent(X,Y),male(X).

%задание сложного предиката (правила) "мать"

mother(X,Y) :- parent(X,Y),female(X).

%задание сложного предиката (правила) "брат"

brother(X,Y) :- parent(Z,X), parent(Z,Y), male(X).

%задание сложного предиката (правила) "сестра"

sister(X,Y) :- parent(Z,X), parent(Z,Y), female(X).

%задание сложного предиката (правила) "ребенок"

child(X.Y):-parent(Y,X).

%задание сложного предиката (правила) "сын"

son(X,Y):- parent(Y,X),male(X).

%задание сложного предиката (правила) "дочь"

daughter(X,Y):- parent(Y,X),female(X).

%задание сложного предиката (правила) "бабушка"

grandmother(X,Y) :- mother(X,Z),parent(Z,Y).

%задание сложного предиката (правила) "дедушка"

grandfather(X,Y) :- father(X,Z),parent(Z,Y).

%задание сложного предиката (правила) "дядя"

%uncle(X, Y):- parent(Parent, Y), brother(X, Parent).

%задание сложного предиката (правила) "тетя"

%aunt(A,Y) :- sister(A,M),mother(M,Y).

%цель1: "Является ли Таня дочерью Александра?"

goal1 :- parent(alexander,tanya),female(tanya).

%цель2: "Является ли Надя бабушкой?"

goal2 :- grandmother(nadya,_), writeln("Надя явлется бабушкой").

%цель3: "Кто является внуком Петра?"
goal3 :- (writeln("Petr's grandchildrens are:");
         parent(petr,X),
         parent(X,Y),
         writeln(Y)),fail.


%цель4: "У кого д/р весной?"

spring(march).
spring(april).
spring(may).

birthday_in_spring(Person) :- birthday(Person,Day,Month,Year),
                               spring(Month), writeln(Person).



							   
%2 лаба 
func(X, Res) :-
    not(number(X)) -> format("Error: ~w is not number", [X]), fail;
    abs(X) > 1 -> format("Error: abs(~w) is more than 1", [X]), fail;
    Res is log(X + (X ** 2 + 1) ** ( 1 / 2 )).

double_factorial(0,0):-!. 
double_factorial(1,1):-!.
double_factorial(2,2):-!.
double_factorial(I,X):-
 I>2,
    I_2 is I - 2,
 double_factorial(I_2,X_2),
 X is I * X_2.


step(X, N, Res) :-
    not(number(X)) -> writeln("X should be number!"), fail;
    abs(X) >= 1 -> writeln("abs(X) should be < 1!"), fail;
    not(integer(N)) -> writeln("N should be integer!"), fail;
    N < 0 ->   writeln("N should be >= 0!"), fail;
    (   
      N = 0 ->  Res is X;
      N > 0 -> (   
        N_1 is (N - 1),
        N_C is (2 * N - 1),
        N_Z is (2 * N),
        step(X, N_1, ResL),
        double_factorial(N_C, C),
        double_factorial(N_Z, Z),
        Res is ((((-1) ** N) * (X ** (2 * N + 1)) * C) / ( Z * (2*N+1)) + ResL)
	)).
	
test(X, N) :-
	func(X, Res_function),
	step(X, N, Res_step),
	format("Ideal result: ~8f~n", [Res_function]),
	format("Result of approximate formula: ~8f~n", [Res_step]),
	format("Absolute calculation error: ~8f", [abs(Res_function - Res_step)]).

