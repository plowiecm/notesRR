
class Note
{
    public Note(string title, string formattedText, string ownerId)
    {
        this.title = title;
        this.formattedText = formattedText;
        this.ownerId = ownerId;
    }

    public string title;
    public string formattedText;
    public string ownerId;
}