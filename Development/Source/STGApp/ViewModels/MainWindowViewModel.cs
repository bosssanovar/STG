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

        #endregion


        #region プロパティ

        #region OwnMachinePositionXラッピングプロパティ
        /// <summary>
        /// 自機位置のX座標を設定または取得します。
        /// </summary>
        public int OwnMachinePositionX => _Model.OwnMachinePosition.X;
        #endregion

        #region OwnMachinePositionYラッピングプロパティ
        /// <summary>
        /// 自機位置のY座標を設定または取得します。
        /// </summary>
        public int OwnMachinePositionY => _Model.OwnMachinePosition.Y;
        #endregion

        #endregion


        #region コマンド

        #region MoveToLeftCommand
        private ViewModelCommand _MoveToLeftCommand;

        /// <summary>
        /// MoveToLeftCommandコマンドを取得します。
        /// </summary>
        public ViewModelCommand MoveToLeftCommand
        {
            get
            {
                if (_MoveToLeftCommand == null)
                {
                    _MoveToLeftCommand = new ViewModelCommand(MoveToLeft);
                }
                return _MoveToLeftCommand;
            }
        }

        /// <summary>
        /// 自機を左に移動します。
        /// </summary>
        public void MoveToLeft()
        {
            _Model.MoveToLeft();
        }
        #endregion

        #region MoveToRightCommand
        private ViewModelCommand _MoveToRightCommand;

        /// <summary>
        /// MoveToRightCommandコマンドを取得します。
        /// </summary>
        public ViewModelCommand MoveToRightCommand
        {
            get
            {
                if (_MoveToRightCommand == null)
                {
                    _MoveToRightCommand = new ViewModelCommand(MoveToRight);
                }
                return _MoveToRightCommand;
            }
        }

        /// <summary>
        /// 自機を右に移動します。
        /// </summary>
        public void MoveToRight()
        {
            _Model.MoveToRight();
        }
        #endregion

        #region MoveToUpCommand
        private ViewModelCommand _MoveToUpCommand;

        /// <summary>
        /// MoveToUpCommandコマンドを取得します。
        /// </summary>
        public ViewModelCommand MoveToUpCommand
        {
            get
            {
                if (_MoveToUpCommand == null)
                {
                    _MoveToUpCommand = new ViewModelCommand(MoveToUp);
                }
                return _MoveToUpCommand;
            }
        }

        /// <summary>
        /// 自機を上に移動します。
        /// </summary>
        public void MoveToUp()
        {
            _Model.MoveToUp();
        }
        #endregion

        #region MoveToDownCommand
        private ViewModelCommand _MoveToDownCommand;

        /// <summary>
        /// MoveToDownCommandコマンドを取得します。
        /// </summary>
        public ViewModelCommand MoveToDownCommand
        {
            get
            {
                if (_MoveToDownCommand == null)
                {
                    _MoveToDownCommand = new ViewModelCommand(MoveToDown);
                }
                return _MoveToDownCommand;
            }
        }

        /// <summary>
        /// 自機を下に移動します。
        /// </summary>
        public void MoveToDown()
        {
            _Model.MoveToDown();
        }
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
