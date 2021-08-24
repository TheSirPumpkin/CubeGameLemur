using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class PlayGamesManager : MonoBehaviour
{
    public UnityEngine.UI.Text DebugText;
    public static PlayGamesManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(this);
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        //    // enables saving game progress.
        //    .EnableSavedGames()
        //    // requests the email address of the player be available.
        //    // Will bring up a prompt for consent.
        //    .RequestEmail()
        //    // requests a server auth code be generated so it can be passed to an
        //    //  associated back end server application and exchanged for an OAuth token.
        //    .RequestServerAuthCode(false)
        //    // requests an ID token be generated.  This OAuth token can be used to
        //    //  identify the player to other services such as Firebase.
        //    .RequestIdToken()
        //    .Build();

        //PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        //PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platforms

        PlayGamesPlatform.Activate();
    }
    private void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            DebugText.text = "" + result;
            //ShowLeader();
        });
    }
    //void ShowLeader()
    //{
    //    Social.ReportScore(100, GPGSIds.leaderboard_top_scores, (bool success) => {
    //        DebugText.text = "LB " + success;
    //        Social.ShowLeaderboardUI();
    //    });

    //}
}
