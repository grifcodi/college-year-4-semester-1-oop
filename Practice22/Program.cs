void PrintValue<T>(T value)
{
    Console.WriteLine(value);
}

var intStack = new MyStack<int>();
var charStack = new MyStack<char>(4);

intStack.Push(0);
intStack.Push(19);
intStack.Push(324);

PrintValue(intStack.Pop());
PrintValue(intStack.Pop());

PrintValue(charStack.Count);

charStack.Push('H');
charStack.Push('e');
charStack.Push('l');
charStack.Push('l');
charStack.Push('o');

PrintValue(charStack.Count);

while (charStack.Count > 0)
{
    PrintValue(charStack.Pop());
}

public class MyStack<T>
{
    private T[] values;
    private int count;
    public int Count => count;

    public MyStack(int length = 8)
    {
        values = new T[length];
        count = 0;
    }
    public void Push(T value)
    {
        if (count == values.Length)
        {
            Array.Resize(ref values, values.Length * 2);
            Console.WriteLine("Resized");
        }
        values[count++] = value;
    }
    public T Pop()
    {
        if (count == 0) throw new InvalidOperationException("Stack is empty.");

        T item = values[--count];
        values[count] = default!;
        return item;
    }
}

//Generic programming дозволив уникати дублювання коду. У випадку чого всю логіку можна легко змінити в одному місці. 