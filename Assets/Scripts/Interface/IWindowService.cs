
public interface IWindowService
{
    void Open(UIWindow window);
    void CloseTop();

    UIWindow CurrentWindow { get; }
    void SelectTopWindow();
}