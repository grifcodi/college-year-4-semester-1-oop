Body body = new Body();

body.Walk();
body.Eat();
body.Live();

public class Organs
{
    public void Breath() => Console.WriteLine("*inhale*");
    public void Beat() => Console.WriteLine("*thump*");
    public void Digest() => Console.WriteLine("*growl*");
}

public class Body
{
    private Organs organs;

    public Body()
    {
        organs = new Organs();
    }

    public void Walk() => organs.Breath();
    public void Eat() => organs.Digest();
    public void Live() => organs.Beat();
}



