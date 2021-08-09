using Game.Exceptions.ValidationExceptions;
using System;

namespace Game.Management.Validators
{
    class NicknameValidator : RegistrationValidator
    {
        public NicknameValidator(string text)
        {
            _text = text;
        }
        public override void Validate()
        {
            if (String.IsNullOrWhiteSpace(_text) || _text.Length < MinimailNicknameLength)
            {
                throw new WrongNicknameException($"��� �� ������ ���� ������ {MinimailNicknameLength} ��������");
            }
            // �������� �� ������� �������� � ��
            //if (_nickName in DB)
            //{
            // throw new ArgumentException("������� ��� �����");
            //}
        }
    }
}
