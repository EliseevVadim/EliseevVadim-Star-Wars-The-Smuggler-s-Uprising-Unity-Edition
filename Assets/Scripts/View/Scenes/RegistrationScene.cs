using Game.Entities;
using Game.Exceptions.ValidationExceptions;
using Game.Management;
using Game.Management.Validators;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.View.Scenes
{
    public class RegistrationScene : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _faces;
        [SerializeField] private Image _faceBox;
        [SerializeField] private InputField _nickNameField;
        [SerializeField] private GameObject _errorPanel;
        [SerializeField] private Text _errorText;
        [SerializeField] private GameObject _userDataRegistration;
        [SerializeField] private GameObject _visualRegistration;
        [SerializeField] private InputField _loginField;
        [SerializeField] private InputField _passwordField;
        [SerializeField] private InputField _confirmPasswordField;
        [SerializeField] private Toggle _passwordToggle;
        [SerializeField] private GameObject _successPanel;

        private RegistrationValidator _validator;
        // навигация по списку аватаров
        private int _index = 0;

        //флаги для визуализации проблем с валидацией
        private bool _wrongNickname = false;
        private bool _wrongPassword = false;
        private bool _wrongLogin = false;

        // данные об игроке
        private string _nickName => _nickNameField.text;
        private string _login => _loginField.text;
        private string _password => _passwordField.text;
        private bool _passwordConfirmed => _passwordField.text == _confirmPasswordField.text;

        private const int StartCredits = 5000;

        public void Start()
        {
            _faceBox.sprite = _faces[_index];
        }
        public void LoadNextFace()
        {
            _index++;
            try
            {
                _faceBox.sprite = _faces[_index];
            }
            catch
            {
                _index = 0;
                _faceBox.sprite = _faces[_index];
            }
        }
        public void LoadPreviousFace()
        {
            _index--;
            try
            {
                _faceBox.sprite = _faces[_index];
            }
            catch
            {
                _index = _faces.Count - 1;
                _faceBox.sprite = _faces[_index];
            }
        }
        public void GoToMenu()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        public void GoNextStep()
        {
            try
            {
                _validator = new NicknameValidator(_nickName);
                _validator.Validate();
                _visualRegistration.SetActive(false);
                _userDataRegistration.SetActive(true);
            }
            catch (WrongNicknameException ex)
            {
                _nickNameField.image.color = Color.red;
                _wrongNickname = true;
                PrepareErrorPanel(ex);
            }
        }
        private void PrepareErrorPanel(Exception ex)
        {
            _errorPanel.SetActive(true);
            if (!_wrongNickname)
            {
                _errorText.text = ex.Message;
            }
            else
            {
                _errorText.text = "Данный никнейм уже использован. Попробуйте другой";
            }
        }
        public void HideErrorPanel()
        {
            _errorPanel.SetActive(false);
            if (_wrongNickname)
            {
                _nickNameField.image.color = Color.white;
                _nickNameField.text = string.Empty;
                _wrongNickname = false;
            }
            if (_wrongPassword)
            {
                _passwordField.image.color = Color.white;
                _passwordField.text = string.Empty;
                _confirmPasswordField.image.color = Color.white;
                _confirmPasswordField.text = string.Empty;
                _wrongPassword = false;
            }
            if (_wrongLogin)
            {
                _loginField.image.color = Color.white;
                _loginField.text = string.Empty;
                _wrongLogin = false;
            }
        }
        public void ChangePasswordFieldsStyle()
        {
            if (_passwordToggle.isOn)
            {
                _passwordField.contentType = InputField.ContentType.Standard;
                _confirmPasswordField.contentType = InputField.ContentType.Standard;
            }
            else
            {
                _passwordField.contentType = InputField.ContentType.Password;
                _confirmPasswordField.contentType = InputField.ContentType.Password;
            }
            _passwordField.ForceLabelUpdate();
            _confirmPasswordField.ForceLabelUpdate();
        }
        public void CreateCharacter()
        {
            try
            {
                _validator = new LoginValidator(_login);
                _validator.Validate();
                _validator = new PasswordValidator(_password, _passwordConfirmed);
                _validator.Validate();
                TextEncryptor encryptor = new TextEncryptor(_password);
                Player player = new Player(0, _nickName, _login, encryptor.GetSha1EncryptedLine(), _index, 0, 0, 0, 1, false, true);
                player.Credits = StartCredits;
                player.AddToDatabase();
                _successPanel.SetActive(true);
            }
            catch (WrongLoginException exception)
            {
                _loginField.image.color = Color.red;
                _wrongLogin = true;
                PrepareErrorPanel(exception);
            }
            catch (WrongPasswordException ex)
            {
                _passwordField.image.color = Color.red;
                _confirmPasswordField.image.color = Color.red;
                _wrongPassword = true;
                PrepareErrorPanel(ex);
            }
            catch (MySqlException existsException)
            {
                _nickNameField.image.color = Color.red;
                _wrongNickname = true;
                PrepareErrorPanel(existsException);
            }
        }
        public void FinishRegistration()
        {
            _successPanel.SetActive(false);
            SceneManager.LoadScene("Menu");
        }
    }
}