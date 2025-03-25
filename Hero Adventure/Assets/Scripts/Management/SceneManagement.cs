public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }
    public void SetTransitionName(string sceneTranstionName)
    {
        this.SceneTransitionName = sceneTranstionName;
    }
}
