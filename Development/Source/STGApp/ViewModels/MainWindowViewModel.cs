using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using STGApp.Models;
using System.Windows.Input;

namespace STGApp.ViewModels
{
	/// <summary>
	/// Main WindowのView Model機能を提供するクラス
	/// </summary>
	public class MainWindowViewModel : ViewModel
	{
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            _Model = new Model();
			_DirectionManager = new DirectionKeyManager();

			AddListaner();
        }

        #endregion


        #region 定数
        #endregion


        #region enum
        #endregion


        #region フィールド

        /// <summary>
        /// <see cref="Model"/>インスタンス
        /// </summary>
        private Model _Model;

        /// <summary>
        /// 直前に入力されたキー
        /// </summary>
        private Key _LastKey = Key.None;

		private DirectionKeyManager _DirectionManager;

        #endregion


        #region プロパティ

        /// <summary>
        /// 自機位置のX座標を設定または取得します。
        /// </summary>
        public int OwnMachinePositionX => _Model.OwnMachinePosition.X;

        /// <summary>
        /// 自機位置のY座標を設定または取得します。
        /// </summary>
        public int OwnMachinePositionY => _Model.OwnMachinePosition.Y;

        /// <summary>
        /// フィールドの幅を取得します。
        /// </summary>
        public int FieldWidth => _Model.FieldSize.X;

        /// <summary>
        /// フィールドの高さを取得します。
        /// </summary>
        public int FieldHeight => _Model.FieldSize.Y;

        #endregion


        #region コマンド

        #region KeyDownCommand
        private ListenerCommand<Key> _KeyDownCommand;

        /// <summary>
        /// Key Downコマンドを取得します。
        /// </summary>
        public ListenerCommand<Key> KeyDownCommand
        {
            get
            {
                if (_KeyDownCommand == null)
                {
                    _KeyDownCommand = new ListenerCommand<Key>(KeyDown);
                }
                return _KeyDownCommand;
            }
        }

        /// <summary>
        /// Key Down時処理
        /// </summary>
        /// <param name="key"></param>
        public void KeyDown(Key key)
        {
            if (_LastKey == key) return;

			_DirectionManager.DirectionChanged += _DirectionManager_DirectionChanged;

			_DirectionManager.SetPressKey(key);

			_DirectionManager.DirectionChanged -= _DirectionManager_DirectionChanged;

			_LastKey = key;
        }

		private void _DirectionManager_DirectionChanged(object sender, DirectionChangedEventArgs e)
		{
			_Model.ClearOrder();
			_Model.SetOrder(e.Order);
		}
		#endregion

		#region KeyUpCommand
		private ListenerCommand<Key> _KeyUpCommand;

        /// <summary>
        /// Key Downコマンドを取得します。
        /// </summary>
        public ListenerCommand<Key> KeyUpCommand
        {
            get
            {
                if (_KeyUpCommand == null)
                {
                    _KeyUpCommand = new ListenerCommand<Key>(KeyUp);
                }
                return _KeyUpCommand;
            }
        }

		/// <summary>
		/// Key Down時処理
		/// </summary>
		/// <param name="key"></param>
		public void KeyUp(Key key)
		{
			_DirectionManager.DirectionChanged += _DirectionManager_DirectionChanged;

			_DirectionManager.SetReleasedKey(key);

			_DirectionManager.DirectionChanged -= _DirectionManager_DirectionChanged;

			if (_LastKey == key) _LastKey = Key.None;
		}
		#endregion

		#region Move Right

		#region AddMoveRightOrderCommand
		private ViewModelCommand _AddMoveRightOrderCommand;

        /// <summary>
        /// 右動作命令追加コマンドを取得します。
        /// </summary>
        public ViewModelCommand AddMoveRightOrderCommand
        {
            get
            {
                if (_AddMoveRightOrderCommand == null)
                {
                    _AddMoveRightOrderCommand = new ViewModelCommand(AddMoveRightOrder);
                }
                return _AddMoveRightOrderCommand;
            }
        }

        /// <summary>
        /// 右動作命令追加を送ります。
        /// </summary>
        public void AddMoveRightOrder()
        {
			_Model.SetOrder(STG.InputManager.Order.MoveRight);
        }
        #endregion

        #region RemoveMoveRightOrderCommand
        private ViewModelCommand _RemoveMoveRightOrderCommand;

        /// <summary>
        /// 右動作命令削除コマンドを取得します。
        /// </summary>
        public ViewModelCommand RemoveMoveRightOrderCommand
        {
            get
            {
                if (_RemoveMoveRightOrderCommand == null)
                {
                    _RemoveMoveRightOrderCommand = new ViewModelCommand(RemoveMoveRightOrder);
                }
                return _RemoveMoveRightOrderCommand;
            }
        }

        /// <summary>
        /// 右動作命令削除を送ります。
        /// </summary>
        public void RemoveMoveRightOrder()
        {
            _Model.ClearOrder();
        }
        #endregion

        #endregion

        #region Move Left

        #region AddMoveLeftOrderCommand
        private ViewModelCommand _AddMoveLeftOrderCommand;

        /// <summary>
        /// 左動作命令追加コマンドを取得します。
        /// </summary>
        public ViewModelCommand AddMoveLeftOrderCommand
        {
            get
            {
                if (_AddMoveLeftOrderCommand == null)
                {
                    _AddMoveLeftOrderCommand = new ViewModelCommand(AddMoveLeftOrder);
                }
                return _AddMoveLeftOrderCommand;
            }
        }

        /// <summary>
        /// 左動作命令追加を送ります。
        /// </summary>
        public void AddMoveLeftOrder()
		{
			_Model.SetOrder(STG.InputManager.Order.MoveLeft);
		}
        #endregion

        #region RemoveMoveLeftOrderCommand
        private ViewModelCommand _RemoveMoveLeftOrderCommand;

        /// <summary>
        /// 左動作命令削除コマンドを取得します。
        /// </summary>
        public ViewModelCommand RemoveMoveLeftOrderCommand
        {
            get
            {
                if (_RemoveMoveLeftOrderCommand == null)
                {
                    _RemoveMoveLeftOrderCommand = new ViewModelCommand(RemoveMoveLeftOrder);
                }
                return _RemoveMoveLeftOrderCommand;
            }
        }

        /// <summary>
        /// 左動作命令削除を送ります。
        /// </summary>
        public void RemoveMoveLeftOrder()
        {
            _Model.ClearOrder();
        }
        #endregion

        #endregion

        #region Move Up

        #region AddMoveUpOrderCommand
        private ViewModelCommand _AddMoveUpOrderCommand;

        /// <summary>
        /// 上動作命令追加コマンドを取得します。
        /// </summary>
        public ViewModelCommand AddMoveUpOrderCommand
        {
            get
            {
                if (_AddMoveUpOrderCommand == null)
                {
                    _AddMoveUpOrderCommand = new ViewModelCommand(AddMoveUpOrder);
                }
                return _AddMoveUpOrderCommand;
            }
        }

        /// <summary>
        /// 上動作命令追加を送ります。
        /// </summary>
        public void AddMoveUpOrder()
		{
			_Model.SetOrder(STG.InputManager.Order.MoveUp);
		}
        #endregion

        #region RemoveMoveUpOrderCommand
        private ViewModelCommand _RemoveMoveUpOrderCommand;

        /// <summary>
        /// 上動作命令削除コマンドを取得します。
        /// </summary>
        public ViewModelCommand RemoveMoveUpOrderCommand
        {
            get
            {
                if (_RemoveMoveUpOrderCommand == null)
                {
                    _RemoveMoveUpOrderCommand = new ViewModelCommand(RemoveMoveUpOrder);
                }
                return _RemoveMoveUpOrderCommand;
            }
        }

        /// <summary>
        /// 上動作命令削除を送ります。
        /// </summary>
        public void RemoveMoveUpOrder()
        {
            _Model.ClearOrder();
        }
        #endregion

        #endregion

        #region Move Down

        #region AddMoveDownOrderCommand
        private ViewModelCommand _AddMoveDownOrderCommand;

        /// <summary>
        /// 下動作命令追加コマンドを取得します。
        /// </summary>
        public ViewModelCommand AddMoveDownOrderCommand
        {
            get
            {
                if (_AddMoveDownOrderCommand == null)
                {
                    _AddMoveDownOrderCommand = new ViewModelCommand(AddMoveDownOrder);
                }
                return _AddMoveDownOrderCommand;
            }
        }

        /// <summary>
        /// 下動作命令追加を送ります。
        /// </summary>
        public void AddMoveDownOrder()
		{
			_Model.SetOrder(STG.InputManager.Order.MoveDown);
		}
        #endregion

        #region RemoveMoveDownOrderCommand
        private ViewModelCommand _RemoveMoveDownOrderCommand;

        /// <summary>
        /// 下動作命令削除コマンドを取得します。
        /// </summary>
        public ViewModelCommand RemoveMoveDownOrderCommand
        {
            get
            {
                if (_RemoveMoveDownOrderCommand == null)
                {
                    _RemoveMoveDownOrderCommand = new ViewModelCommand(RemoveMoveDownOrder);
                }
                return _RemoveMoveDownOrderCommand;
            }
        }

        /// <summary>
        /// 下動作命令削除を送ります。
        /// </summary>
        public void RemoveMoveDownOrder()
        {
            _Model.ClearOrder();
        }
        #endregion

        #endregion



        #endregion


        #region デリゲート
        #endregion


        #region イベント
        #endregion


        #region イベントハンドラ
        #endregion


        #region メソッド

        /// <summary>
        /// 画面表示時に実行されるメソッドです。
        /// </summary>
        public void Initialize()
        {
            _Model.StartGame();
        }

        /// <summary>
        /// Listanerの購読設定を行います。
        /// </summary>
        private void AddListaner()
        {
            CompositeDisposable.Add(new PropertyChangedEventListener(_Model,
                (sender, e) =>
                {
                    var model = sender as Model;

                    if(e.PropertyName == nameof(model.OwnMachinePosition))
                    {
                        RaisePropertyChanged(nameof(this.OwnMachinePositionX));
                        RaisePropertyChanged(nameof(this.OwnMachinePositionY));
                    }

                }));
        }

        #endregion


        #region 内部クラス
        #endregion
    }
}
