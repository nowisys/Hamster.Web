using Hamster.Messaging.Services;
using Hamster.Plugin;
using Hamster.Plugin.IoC;
using System;
using System.Xml;

namespace Hamster.Messaging
{
    public class Plugin : AbstractPlugin<PluginSettings>
    {
        private IUserService _userService;
        private IMessageService _messageService;

        public override void Configure(XmlElement element)
        {
            base.Configure(element);
        }

        public void Bind(string name, object instance)
        {
            // Bind infrastructure plugins (database, com, ...)
            _mongoDb = (DbPlugin)instance;
        }

        public void BindingComplete()
        {

        }

        public override void Init()
        {
            PluginServiceProvider provider = new PluginServiceProvider();
            _userService = new UserService();
            _messageService = new MessageService(_userService);
            provider.AddSingleton<IUserService>(_userService);
            provider.AddSingleton<IMessageService>(_messageService);
            PluginServiceProvider = provider;

            base.Init();
        }

        public override void Open()
        {
            base.Open();
            // _userService.DoSomething();
            // _messageService.DoSomething();
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
