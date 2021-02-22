namespace QuestManagmentLib
{
    /// <summary>
    /// Just user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Name of the User.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Passing user's name.
        /// </summary>
        /// <param name="name"> <inheritdoc cref="Name"/> </param>
        public User(string name)
        {
            Name = name;
        }
    }
}