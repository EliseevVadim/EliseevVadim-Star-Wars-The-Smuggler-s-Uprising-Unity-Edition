using Game.Management;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.View.Scenes
{
    public class AuthorizationScene : MonoBehaviour
    {
        [SerializeField] private InputField _nicknameField;
        [SerializeField] private InputField _loginField;
        [SerializeField] private InputField _passwordField;
        [SerializeField] private Toggle _passwordToggle;
        [SerializeField] private GameObject _successMessage;
        [SerializeField] private GameObject _errorMessage;
        [SerializeField] private Text _errorText;
        [SerializeField] private GameObject _loadingScreen;

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        public void ChangePasswordFieldStyle()
        {
            if (_passwordToggle.isOn)
            {
                _passwordField.contentType = InputField.ContentType.Standard;
            }
            else
            {
                _passwordField.contentType = InputField.ContentType.Password;
            }
            _passwordField.ForceLabelUpdate();
        }
        public void StartGame()
        {
            try
            {
                Authorizator authorizator = new Authorizator(_nicknameField.text, _loginField.text, _passwordField.text);
                if (authorizator.UserExists())
                {
                    _successMessage.SetActive(true);
                }
                else
                {
                    _errorMessage.SetActive(true);
                    _errorText.text = "Ошибка входа. Проверьте введенные данные.";
                }
            }
            catch (Exception ex)
            {
                _errorMessage.SetActive(true);
                _errorText.text = ex.Message;
            }
        }
        public void ProcessToGame()
        {
            _successMessage.SetActive(false);
            _loadingScreen.SetActive(true);
        }
    }
}
