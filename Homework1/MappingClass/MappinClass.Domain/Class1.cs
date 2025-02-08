namespace MappinClass.Domain;

public class MiembroDeLaComunidad
{
    public string Nombre { get; set; }

    public MiembroDeLaComunidad(string nombre)
    {
        Nombre = nombre;
    }

    public virtual void EnsenarInformacion()
    {
        Console.WriteLine($"Nombre: {Nombre}");
    }
}

public class Empleado : MiembroDeLaComunidad
{
    public Empleado(string nombre) : base(nombre) { }
}

public class Estudiante : MiembroDeLaComunidad
{
    public Estudiante(string nombre) : base(nombre) { }
}

public class ExAlumno : MiembroDeLaComunidad
{
    public ExAlumno(string nombre) : base(nombre) { }
}

public class Docente : Empleado
{
    public Docente(string nombre) : base(nombre) { }
}

public class Administrativo : Empleado
{
    public Administrativo(string nombre) : base(nombre) { }
}

public class Administrador : Docente
{
    public Administrador(string nombre) : base(nombre) { }
}

public class Maestro : Docente
{
    public Maestro(string nombre) : base(nombre) { }
}
