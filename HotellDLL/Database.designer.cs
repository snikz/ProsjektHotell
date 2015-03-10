﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotellDLL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="HotelManagement")]
	public partial class DatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBooking(Booking instance);
    partial void UpdateBooking(Booking instance);
    partial void DeleteBooking(Booking instance);
    partial void InsertGuest(Guest instance);
    partial void UpdateGuest(Guest instance);
    partial void DeleteGuest(Guest instance);
    partial void InsertRoom(Room instance);
    partial void UpdateRoom(Room instance);
    partial void DeleteRoom(Room instance);
    partial void InsertService(Service instance);
    partial void UpdateService(Service instance);
    partial void DeleteService(Service instance);
    #endregion
		
		public DatabaseDataContext() : 
				base(global::HotellDLL.Properties.Settings.Default.HotelManagementConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Booking> Bookings
		{
			get
			{
				return this.GetTable<Booking>();
			}
		}
		
		public System.Data.Linq.Table<Guest> Guests
		{
			get
			{
				return this.GetTable<Guest>();
			}
		}
		
		public System.Data.Linq.Table<Room> Rooms
		{
			get
			{
				return this.GetTable<Room>();
			}
		}
		
		public System.Data.Linq.Table<Service> Services
		{
			get
			{
				return this.GetTable<Service>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Booking")]
	public partial class Booking : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _bookingId;
		
		private System.DateTime _checkInDate;
		
		private System.DateTime _checkOutDate;
		
		private int _guestId;
		
		private int _roomId;
		
		private bool _checkedIn;
		
		private bool _checkedOut;
		
		private EntityRef<Guest> _Guest;
		
		private EntityRef<Room> _Room;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnbookingIdChanging(int value);
    partial void OnbookingIdChanged();
    partial void OncheckInDateChanging(System.DateTime value);
    partial void OncheckInDateChanged();
    partial void OncheckOutDateChanging(System.DateTime value);
    partial void OncheckOutDateChanged();
    partial void OnguestIdChanging(int value);
    partial void OnguestIdChanged();
    partial void OnroomIdChanging(int value);
    partial void OnroomIdChanged();
    partial void OncheckedInChanging(bool value);
    partial void OncheckedInChanged();
    partial void OncheckedOutChanging(bool value);
    partial void OncheckedOutChanged();
    #endregion
		
		public Booking()
		{
			this._Guest = default(EntityRef<Guest>);
			this._Room = default(EntityRef<Room>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bookingId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int bookingId
		{
			get
			{
				return this._bookingId;
			}
			set
			{
				if ((this._bookingId != value))
				{
					this.OnbookingIdChanging(value);
					this.SendPropertyChanging();
					this._bookingId = value;
					this.SendPropertyChanged("bookingId");
					this.OnbookingIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_checkInDate", DbType="DateTime NOT NULL")]
		public System.DateTime checkInDate
		{
			get
			{
				return this._checkInDate;
			}
			set
			{
				if ((this._checkInDate != value))
				{
					this.OncheckInDateChanging(value);
					this.SendPropertyChanging();
					this._checkInDate = value;
					this.SendPropertyChanged("checkInDate");
					this.OncheckInDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_checkOutDate", DbType="DateTime NOT NULL")]
		public System.DateTime checkOutDate
		{
			get
			{
				return this._checkOutDate;
			}
			set
			{
				if ((this._checkOutDate != value))
				{
					this.OncheckOutDateChanging(value);
					this.SendPropertyChanging();
					this._checkOutDate = value;
					this.SendPropertyChanged("checkOutDate");
					this.OncheckOutDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_guestId", DbType="Int NOT NULL")]
		public int guestId
		{
			get
			{
				return this._guestId;
			}
			set
			{
				if ((this._guestId != value))
				{
					if (this._Guest.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnguestIdChanging(value);
					this.SendPropertyChanging();
					this._guestId = value;
					this.SendPropertyChanged("guestId");
					this.OnguestIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_roomId", DbType="Int NOT NULL")]
		public int roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				if ((this._roomId != value))
				{
					if (this._Room.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnroomIdChanging(value);
					this.SendPropertyChanging();
					this._roomId = value;
					this.SendPropertyChanged("roomId");
					this.OnroomIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_checkedIn", DbType="Bit NOT NULL")]
		public bool checkedIn
		{
			get
			{
				return this._checkedIn;
			}
			set
			{
				if ((this._checkedIn != value))
				{
					this.OncheckedInChanging(value);
					this.SendPropertyChanging();
					this._checkedIn = value;
					this.SendPropertyChanged("checkedIn");
					this.OncheckedInChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_checkedOut", DbType="Bit NOT NULL")]
		public bool checkedOut
		{
			get
			{
				return this._checkedOut;
			}
			set
			{
				if ((this._checkedOut != value))
				{
					this.OncheckedOutChanging(value);
					this.SendPropertyChanging();
					this._checkedOut = value;
					this.SendPropertyChanged("checkedOut");
					this.OncheckedOutChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Guest_Booking", Storage="_Guest", ThisKey="guestId", OtherKey="guestId", IsForeignKey=true)]
		public Guest Guest
		{
			get
			{
				return this._Guest.Entity;
			}
			set
			{
				Guest previousValue = this._Guest.Entity;
				if (((previousValue != value) 
							|| (this._Guest.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Guest.Entity = null;
						previousValue.Bookings.Remove(this);
					}
					this._Guest.Entity = value;
					if ((value != null))
					{
						value.Bookings.Add(this);
						this._guestId = value.guestId;
					}
					else
					{
						this._guestId = default(int);
					}
					this.SendPropertyChanged("Guest");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Room_Booking", Storage="_Room", ThisKey="roomId", OtherKey="roomId", IsForeignKey=true)]
		public Room Room
		{
			get
			{
				return this._Room.Entity;
			}
			set
			{
				Room previousValue = this._Room.Entity;
				if (((previousValue != value) 
							|| (this._Room.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Room.Entity = null;
						previousValue.Bookings.Remove(this);
					}
					this._Room.Entity = value;
					if ((value != null))
					{
						value.Bookings.Add(this);
						this._roomId = value.roomId;
					}
					else
					{
						this._roomId = default(int);
					}
					this.SendPropertyChanged("Room");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Guest")]
	public partial class Guest : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _guestId;
		
		private string _firstName;
		
		private string _lastName;
		
		private string _email;
		
		private string _password;
		
		private EntitySet<Booking> _Bookings;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnguestIdChanging(int value);
    partial void OnguestIdChanged();
    partial void OnfirstNameChanging(string value);
    partial void OnfirstNameChanged();
    partial void OnlastNameChanging(string value);
    partial void OnlastNameChanged();
    partial void OnemailChanging(string value);
    partial void OnemailChanged();
    partial void OnpasswordChanging(string value);
    partial void OnpasswordChanged();
    #endregion
		
		public Guest()
		{
			this._Bookings = new EntitySet<Booking>(new Action<Booking>(this.attach_Bookings), new Action<Booking>(this.detach_Bookings));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_guestId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int guestId
		{
			get
			{
				return this._guestId;
			}
			set
			{
				if ((this._guestId != value))
				{
					this.OnguestIdChanging(value);
					this.SendPropertyChanging();
					this._guestId = value;
					this.SendPropertyChanged("guestId");
					this.OnguestIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if ((this._firstName != value))
				{
					this.OnfirstNameChanging(value);
					this.SendPropertyChanging();
					this._firstName = value;
					this.SendPropertyChanged("firstName");
					this.OnfirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="VarChar(30) NOT NULL", CanBeNull=false)]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if ((this._lastName != value))
				{
					this.OnlastNameChanging(value);
					this.SendPropertyChanging();
					this._lastName = value;
					this.SendPropertyChanged("lastName");
					this.OnlastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email", DbType="VarChar(40) NOT NULL", CanBeNull=false)]
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this.OnemailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("email");
					this.OnemailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_password", DbType="VarChar(15)")]
		public string password
		{
			get
			{
				return this._password;
			}
			set
			{
				if ((this._password != value))
				{
					this.OnpasswordChanging(value);
					this.SendPropertyChanging();
					this._password = value;
					this.SendPropertyChanged("password");
					this.OnpasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Guest_Booking", Storage="_Bookings", ThisKey="guestId", OtherKey="guestId")]
		public EntitySet<Booking> Bookings
		{
			get
			{
				return this._Bookings;
			}
			set
			{
				this._Bookings.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Bookings(Booking entity)
		{
			this.SendPropertyChanging();
			entity.Guest = this;
		}
		
		private void detach_Bookings(Booking entity)
		{
			this.SendPropertyChanging();
			entity.Guest = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Room")]
	public partial class Room : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _roomId;
		
		private int _bed;
		
		private int _price;
		
		private int _size;
		
		private int _quality;
		
		private EntitySet<Booking> _Bookings;
		
		private EntitySet<Service> _Services;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnroomIdChanging(int value);
    partial void OnroomIdChanged();
    partial void OnbedChanging(int value);
    partial void OnbedChanged();
    partial void OnpriceChanging(int value);
    partial void OnpriceChanged();
    partial void OnsizeChanging(int value);
    partial void OnsizeChanged();
    partial void OnqualityChanging(int value);
    partial void OnqualityChanged();
    #endregion
		
		public Room()
		{
			this._Bookings = new EntitySet<Booking>(new Action<Booking>(this.attach_Bookings), new Action<Booking>(this.detach_Bookings));
			this._Services = new EntitySet<Service>(new Action<Service>(this.attach_Services), new Action<Service>(this.detach_Services));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_roomId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				if ((this._roomId != value))
				{
					this.OnroomIdChanging(value);
					this.SendPropertyChanging();
					this._roomId = value;
					this.SendPropertyChanged("roomId");
					this.OnroomIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bed", DbType="Int NOT NULL")]
		public int bed
		{
			get
			{
				return this._bed;
			}
			set
			{
				if ((this._bed != value))
				{
					this.OnbedChanging(value);
					this.SendPropertyChanging();
					this._bed = value;
					this.SendPropertyChanged("bed");
					this.OnbedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_price", DbType="Int NOT NULL")]
		public int price
		{
			get
			{
				return this._price;
			}
			set
			{
				if ((this._price != value))
				{
					this.OnpriceChanging(value);
					this.SendPropertyChanging();
					this._price = value;
					this.SendPropertyChanged("price");
					this.OnpriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_size", DbType="Int NOT NULL")]
		public int size
		{
			get
			{
				return this._size;
			}
			set
			{
				if ((this._size != value))
				{
					this.OnsizeChanging(value);
					this.SendPropertyChanging();
					this._size = value;
					this.SendPropertyChanged("size");
					this.OnsizeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_quality", DbType="Int NOT NULL")]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				if ((this._quality != value))
				{
					this.OnqualityChanging(value);
					this.SendPropertyChanging();
					this._quality = value;
					this.SendPropertyChanged("quality");
					this.OnqualityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Room_Booking", Storage="_Bookings", ThisKey="roomId", OtherKey="roomId")]
		public EntitySet<Booking> Bookings
		{
			get
			{
				return this._Bookings;
			}
			set
			{
				this._Bookings.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Room_Service", Storage="_Services", ThisKey="roomId", OtherKey="roomId")]
		public EntitySet<Service> Services
		{
			get
			{
				return this._Services;
			}
			set
			{
				this._Services.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Bookings(Booking entity)
		{
			this.SendPropertyChanging();
			entity.Room = this;
		}
		
		private void detach_Bookings(Booking entity)
		{
			this.SendPropertyChanging();
			entity.Room = null;
		}
		
		private void attach_Services(Service entity)
		{
			this.SendPropertyChanging();
			entity.Room = this;
		}
		
		private void detach_Services(Service entity)
		{
			this.SendPropertyChanging();
			entity.Room = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Service")]
	public partial class Service : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private int _type;
		
		private int _roomId;
		
		private string _note;
		
		private bool _status;
		
		private EntityRef<Room> _Room;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OntypeChanging(int value);
    partial void OntypeChanged();
    partial void OnroomIdChanging(int value);
    partial void OnroomIdChanged();
    partial void OnnoteChanging(string value);
    partial void OnnoteChanged();
    partial void OnstatusChanging(bool value);
    partial void OnstatusChanged();
    #endregion
		
		public Service()
		{
			this._Room = default(EntityRef<Room>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_type", DbType="Int NOT NULL")]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				if ((this._type != value))
				{
					this.OntypeChanging(value);
					this.SendPropertyChanging();
					this._type = value;
					this.SendPropertyChanged("type");
					this.OntypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_roomId", DbType="Int NOT NULL")]
		public int roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				if ((this._roomId != value))
				{
					if (this._Room.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnroomIdChanging(value);
					this.SendPropertyChanging();
					this._roomId = value;
					this.SendPropertyChanged("roomId");
					this.OnroomIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_note", DbType="VarChar(MAX)")]
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this.OnnoteChanging(value);
					this.SendPropertyChanging();
					this._note = value;
					this.SendPropertyChanged("note");
					this.OnnoteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_status", DbType="Bit NOT NULL")]
		public bool status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this.OnstatusChanging(value);
					this.SendPropertyChanging();
					this._status = value;
					this.SendPropertyChanged("status");
					this.OnstatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Room_Service", Storage="_Room", ThisKey="roomId", OtherKey="roomId", IsForeignKey=true)]
		public Room Room
		{
			get
			{
				return this._Room.Entity;
			}
			set
			{
				Room previousValue = this._Room.Entity;
				if (((previousValue != value) 
							|| (this._Room.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Room.Entity = null;
						previousValue.Services.Remove(this);
					}
					this._Room.Entity = value;
					if ((value != null))
					{
						value.Services.Add(this);
						this._roomId = value.roomId;
					}
					else
					{
						this._roomId = default(int);
					}
					this.SendPropertyChanged("Room");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
