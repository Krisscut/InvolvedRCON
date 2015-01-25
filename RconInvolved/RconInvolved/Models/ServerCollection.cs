
namespace RconInvolved.Models
{
    using System;
    using System.Collections.ObjectModel;

    public sealed class ServerCollection : ObservableCollection<Server>
    {
        public ServerCollection() : this(false)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ServerCollection"/> class.
        /// </summary>
        public ServerCollection(bool hasErrors)
        {
            this.Add(
                new Server(this)
                {
                    Date = DateTime.Today.AddDays(2),
                    Icon = "Icon 1",
                    IsActive = true,
                    Name = "Name 1",
                    Url = "http://www.google.com",
                    Priority = 1,
                    Value = 2132332
                });
            this.Add(
                new Server(this)
                {
                    Date = DateTime.Today.AddDays(1),
                    Icon = "Icon 2",
                    IsActive = true,
                    Name = "Name 2",
                    Url = "http://www.google.com",
                    Priority = 2,
                    Value = 423434
                });
            this.Add(
                new Server(this)
                {
                    Date = DateTime.Today.AddDays(1),
                    Icon = "Icon 2",
                    IsActive = true,
                    Name = "Name 2",
                    Url = "http://www.google.com",
                    Priority = 2,
                    Value = 423434
                });
            this.Add(
                new Server(this)
                {
                    Date = DateTime.Today.AddDays(1),
                    Icon = "Icon 2",
                    IsActive = true,
                    Name = "Name 2",
                    Url = "http://www.google.com",
                    Priority = 2,
                    Value = 423434
                });

            if (hasErrors)
            {
                this.Add(
                    new Server(this)
                    {
                        Date = DateTime.Today,
                        Icon = "Icon 6",
                        IsActive = false,
                        Name = "Name 6",
                        Url = "http://www.google.com",
                        Priority = 6,
                        Value = -1
                    });
                this.Add(
                    new Server(this)
                    {
                        Date = DateTime.Today.AddDays(-1),
                        Priority = 7,
                    });
            }
        }
    }
    
}



