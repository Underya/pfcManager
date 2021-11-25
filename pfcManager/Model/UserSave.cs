using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pfcManager.Model
{
    /// <summary>
    /// Сохранение информации о пользователе в виде соли
    /// </summary>
    public class UserSave : UsersDB
    {
        /// <summary>
        /// Сохранение пароля в закрытом виде
        /// </summary>
        /// <param name="password"></param>
        public void SetHashPassword(string password)
        {
            string hash = "", salt = "";
            GetPasswordSalt(password, out hash, out salt);
            this.Hash = hash;
            this.Salt = salt;
        }

        public UserSave(UsersDB user)
        {
            SetInfoUser(user);
        }

        /// <summary>
        /// Получение информации из базового объекта
        /// </summary>
        /// <param name="user"></param>
        void SetInfoUser(UsersDB user)
        {
            this.Firstname = user.Firstname;
            this.Datebridh = user.Datebridh;
            this.Lastname = user.Lastname;
            this.Midname = user.Midname;
            this.Height = user.Height;
            this.Salt = user.Salt;
            this.Hash = user.Hash;
            this.Sex = user.Sex;
            this.Id = user.Id;
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        public UserSave()
        {

        }

        /// <summary>
        /// Получение пользователя с указанным логином
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static UserSave GetUser (string login)
        {
            using (ModelContext tc = new ModelContext())
            {
                UserSave us = null; 
                UsersDB users = tc.Users.Where(p => p.Login == login).FirstOrDefault();
                if (users == null) return null;
                us = new UserSave(users);
                return us;
            }
        }

        /// <summary>
        /// Проверка, есть ли пользователь с таким логиным и паролем
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckUser(string login, string password)
        {
            using (ModelContext tc = new ModelContext())
            {
                UsersDB u = GetUser(login); 

                if (u == null) return false;

                return CheckPass(password, u.Hash, u.Salt);
            }

        }

        /// <summary>
        /// Возвращение количества лет пользователя
        /// </summary>
        public int YearsOld
        {
            get
            {
                //Если нет указан возвраст, то считается средний по умолчанию
                if (Datebridh == null)
                    return 25;

                //Расчёт количества лет
                DateTime now = DateTime.Now;
                return now.Year - Datebridh.Value.Year;
            }
        }

        /// <summary>
        /// Получение по открытому параметру хешу и соли
        /// </summary>
        /// <param name="OpenPassword">Открытый пароль</param>
        /// <param name="Hash">Возвращаемый хеш от пароля</param>
        /// <param name="Salt">Соль, по которой сделан пароль</param>
        static void GetPasswordSalt(string OpenPassword, out string Hash , out string Salt)
        {
            SaltGenerator sg = new SaltGenerator();
            sg.GetOpenPassword(OpenPassword, out Hash, out Salt);
        }

        /// <summary>
        /// Проверка открытого пароля на совпадание со старым паролем при указанной соли
        /// </summary>
        /// <param name="OpenPassword"></param>
        /// <param name="Hash"></param>
        /// <param name="Salt"></param>
        /// <returns></returns>
        static bool CheckPass(string OpenPassword, string Hash, string Salt)
        {
            SaltGenerator sg = new SaltGenerator();
            string hash2 = sg.GetHash(OpenPassword, Salt);
            return Hash == hash2;
        }
    }
}
