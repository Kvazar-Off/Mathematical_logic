
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
writeln("\n�������� � ����� ������ ����������� ����������:"),
writef("1 - ������ � ������������� ������\n2 - ������ � ������������� ������\n3 - ���������� ������\n").

main_menu :-
writeln("\n�������� �������� �� ������:"),
writef("1 - ������ � �������� ������\n2 - ���������� ������� arctg(x)\n3 - ���������� ������ ���������\n").

fam_menu :-
writeln("\n�������� �������� �� ������:"),
writef("1 - �������� �� ���� ������� ����������?\n2 - �������� �� ���� ��������?\n3 - ��� �������� ������ �����?\n4 - � ���� �/� ������?\n5 - �����\n").

func_menu :-
writeln("\n�������� �������� �� ������:"),
writef("1 - ������ ���������� �������\n2 - ���������� ������� �� ������������ ������� \n3 - ��������� ��������� �������� � �������������\n4 - �����\n").

func_check([Command]) :-
Command = 1 ->( func_menu,
readln(A),
func_check_pos(A), fail
);
Command = 2 ->( func_menu,
readln(A),
func_check_neg(A), fail
);
Command = 3 -> writeln("�����"), !.

check([Command]) :-
Command = 1 -> (fam, fail);
Command = 2 -> (func, fail);
Command = 3 -> writeln("��������� ���������").

fam_check([Command]) :-
Command = 1 -> (goal1, fail);
Command = 2 -> (goal2, fail);
Command = 3 -> (goal3, fail);
Command = 4 -> (birthday_in_spring(Person), fail);
Command = 5 -> writeln("�����"), !.


func_check_pos([Command]) :-
Command = 1 -> (
writeln("������� X:"), readln(ListX), ListX = [X],
func(X, Res),
format("X = ~w. ������� = ~w", [X, Res]),
fail
);
Command = 2 -> (
writeln("������� X:"), readln(ListX), ListX = [X],
writeln("������� N:"), readln(ListN), ListN = [N],
step(X, N, Res),
format("X = ~w and N = ~w. ������� = ~w", [X, N, Res]),
fail
);
Command = 3 -> (
writeln("������� X:"), readln(ListX), ListX = [X],
writeln("������� N:"), readln(ListN), ListN = [N],
test(X, N),
fail
);
Command = 4 -> writeln("�����"), !.


func_check_neg([Command]) :-
Command = 1 -> (
writeln("������� ������ X:"), readln(ListX), ListX = [X],
X_1 is X * -1,
func(X_1, Res), 
format("X = ~w. ������� = ~w", [X, Res]),
fail
);
Command = 2 -> (
writeln("������� ������ X:"), readln(ListX), ListX = [X],
writeln("������� N:"), readln(ListN), ListN = [N],
X_1 is X * -1,
step(X_1, N, Res), 
format("X = ~w and N = ~w. ������� = ~w", [X_1, N, Res]),
fail
);
Command = 3 -> (
writeln("������� ������ X:"), readln(ListX), ListX = [X],
writeln("������� N:"), readln(ListN), ListN = [N],
X_1 is X * -1,
test((X_1), N),
fail
);
Command = 4 -> writeln("����� �� ������ ������ � ��������"), !.






%1 ����% ������� �������� ����������� ��������� (������) "��������"

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

% ������� �������� ������������ ��������� (������) "�������"

male(petr).
male(alexey).
male(sergey).
male(ivan).
male(alexander).
male(egor).

% ������� �������� ������������ ��������� (������) "�������"

female(nadya).
female(larisa).
female(marina).
female(tanya).


% ������� �������� ����������� ��������� (������) "����� ��������"

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


% ������� �������� 4-�������� ��������� "���� ��������"

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

%������� �������� ��������� (�������) "����"

father(X,Y) :- parent(X,Y),male(X).

%������� �������� ��������� (�������) "����"

mother(X,Y) :- parent(X,Y),female(X).

%������� �������� ��������� (�������) "����"

brother(X,Y) :- parent(Z,X), parent(Z,Y), male(X).

%������� �������� ��������� (�������) "������"

sister(X,Y) :- parent(Z,X), parent(Z,Y), female(X).

%������� �������� ��������� (�������) "�������"

child(X.Y):-parent(Y,X).

%������� �������� ��������� (�������) "���"

son(X,Y):- parent(Y,X),male(X).

%������� �������� ��������� (�������) "����"

daughter(X,Y):- parent(Y,X),female(X).

%������� �������� ��������� (�������) "�������"

grandmother(X,Y) :- mother(X,Z),parent(Z,Y).

%������� �������� ��������� (�������) "�������"

grandfather(X,Y) :- father(X,Z),parent(Z,Y).

%������� �������� ��������� (�������) "����"

%uncle(X, Y):- parent(Parent, Y), brother(X, Parent).

%������� �������� ��������� (�������) "����"

%aunt(A,Y) :- sister(A,M),mother(M,Y).

%����1: "�������� �� ���� ������� ����������?"

goal1 :- parent(alexander,tanya),female(tanya).

%����2: "�������� �� ���� ��������?"

goal2 :- grandmother(nadya,_), writeln("���� ������� ��������").

%����3: "��� �������� ������ �����?"
goal3 :- (writeln("Petr's grandchildrens are:");
         parent(petr,X),
         parent(X,Y),
         writeln(Y)),fail.


%����4: "� ���� �/� ������?"

spring(march).
spring(april).
spring(may).

birthday_in_spring(Person) :- birthday(Person,Day,Month,Year),
                               spring(Month), writeln(Person).



							   
%2 ���� 
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

