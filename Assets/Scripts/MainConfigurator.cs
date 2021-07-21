using Winter.Attributes;

public interface ISomeInterface 
{
    //int GetInt();
}

public class SomeImpl : ISomeInterface 
{

}

[Controller]
public class InjectSomeInterface
{
    [Inject]
    private ISomeInterface m_SomeInterface;

    public ISomeInterface SomeInterface { get => m_SomeInterface; }
}

[Configurator]
public class MainConfigurator 
{
    [Bean(typeof(ISomeInterface), typeof(SomeImpl))]
    private void Config1()
    {

    }
}
