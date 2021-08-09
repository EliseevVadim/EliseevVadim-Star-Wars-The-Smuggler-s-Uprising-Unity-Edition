namespace Game.Management.Validators
{
    abstract class RegistrationValidator
    {
        protected string _text;
        protected const int MinimailNicknameLength = 3;
        protected const int MinimalPasswordLength = 6;
        public abstract void Validate();
    }
}
