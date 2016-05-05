using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Xml;
using System.Collections.ObjectModel;

namespace PartyOrganizer
{
    public static class XmlParser
    {
        public static void SaveXML(string xmlSavePath, ObservableCollection<Guest> guestsList)
        {
            XmlDocument xmlDoc = CreateXmlDocument(guestsList);
            xmlDoc.Save(xmlSavePath);
        }

        private static XmlDocument CreateXmlDocument(ObservableCollection<Guest> gList)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement guestsList = doc.CreateElement(string.Empty, "GuestsList", string.Empty);
            doc.AppendChild(guestsList);

            foreach (var item in gList)
            {
                XmlElement guest = doc.CreateElement(string.Empty, "Guest", string.Empty);
                guestsList.AppendChild(guest);

                XmlElement name = doc.CreateElement(string.Empty, "Name", string.Empty);
                XmlText txtName = doc.CreateTextNode(item.Name);
                name.AppendChild(txtName);
                guest.AppendChild(name);

                XmlElement surname = doc.CreateElement(string.Empty, "Surname", string.Empty);
                XmlText txtSurname = doc.CreateTextNode(item.Surname);
                surname.AppendChild(txtSurname);
                guest.AppendChild(surname);

                XmlElement email = doc.CreateElement(string.Empty, "Email", string.Empty);
                XmlText txtEmail = doc.CreateTextNode(item.Email);
                email.AppendChild(txtEmail);
                guest.AppendChild(email);

                XmlElement sex = doc.CreateElement(string.Empty, "Sex", string.Empty);
                XmlText txtSex = doc.CreateTextNode( item.Sex );
                sex.AppendChild(txtSex);
                guest.AppendChild(sex);

                XmlElement confirmed = doc.CreateElement(string.Empty, "Confirmed", string.Empty);
                XmlText txtConfirmed = doc.CreateTextNode((item.Confirmed) ? "Yes" : "No");
                confirmed.AppendChild(txtConfirmed);
                guest.AppendChild(confirmed);
            }
            return doc;
        }
    }
}
