using RadioConformanceTests.Drivers;
public interface IScpiClient
{
    /// One function one functionality
    void Command(string cmd);
    string Query(string query);
}