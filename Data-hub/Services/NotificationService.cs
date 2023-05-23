using FireSharp.Interfaces;
using FireSharp.Response;

namespace Data_hub.Services;

public class NotificationService
{
    private readonly IFirebaseClient _firebaseClient;

    public NotificationService(IFirebaseClient firebaseClient)
    {
        _firebaseClient = firebaseClient;
    }

    public async Task AddUserToNotifyWhenFreeList(string unique, string coworkerEmail)
    {
        // Retrieve the coworker's notifyWhenFree list from Firebase
        List<string> notifyWhenFree = await GetNotifyWhenFreeList(coworkerEmail);

        // Check if the user is already in the coworker's notifyWhenFree list
        if (notifyWhenFree.Contains(unique))
        {
            throw new Exception("You have already requested to be notified when this coworker is free.");
        }

        // Add the user to the coworker's notifyWhenFree list
        notifyWhenFree.Add(unique);

        // Update the coworker's notifyWhenFree list in Firebase
        await UpdateNotifyWhenFreeList(coworkerEmail, notifyWhenFree);
    }

    private async Task<List<string>> GetNotifyWhenFreeList(string coworkerEmail)
    {
        FirebaseResponse response = await _firebaseClient.GetAsync($"users/{coworkerEmail}/notifyWhenFree");
        List<string> notifyWhenFree = response.ResultAs<List<string>>();

        // If the list is null, initialize it
        if (notifyWhenFree == null)
        {
            notifyWhenFree = new List<string>();
        }

        return notifyWhenFree;
    }

    private async Task UpdateNotifyWhenFreeList(string coworkerEmail, List<string> notifyWhenFree)
    {
        await _firebaseClient.UpdateAsync($"users/{coworkerEmail}/notifyWhenFree", notifyWhenFree);
    }
}
