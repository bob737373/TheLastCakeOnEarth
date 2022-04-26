interface IPersistentObject
{
    string persistent_unique_id {get; set;}
    void shouldSpawn();
    void generateID();
    void setObjectUsed();
}