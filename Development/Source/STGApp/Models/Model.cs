﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using STG.MachinePosition;
using STG.Parameters;

namespace STGApp.Models
{
    /// <summary>
    /// MainWindowのModel機能を提供するクラス
    /// </summary>
    public class Model : NotificationObject
    {
        #region コンストラクタ/デストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Model()
        {
            InitInstances();
        }

        #endregion


        #region 定数

        /// <summary>
        /// フィールドの幅
        /// </summary>
        private const int FieldSizeX = 500;

        /// <summary>
        /// フィールドの高さ
        /// </summary>
        private const int FieldSizeY = 300;

        /// <summary>
        /// 自機の初期位置X座標
        /// </summary>
        private const int OwnMachineInitialPositionX = 100;

        /// <summary>
        /// 自機の初期位置Y座標
        /// </summary>
        private const int OwnMachineInitialPositionY = 100;

        #endregion


        #region enum
        #endregion


        #region フィールド

        /// <summary>
        /// 自機の位置を保持します。※VMに対して通知が必要な場合は<see cref="OwnMachinePosition"/>プロパティのSetアクセサを使用してください。
        /// </summary>
        private Position _OwnMachinePosition = new Position(OwnMachineInitialPositionX, OwnMachineInitialPositionY);

        /// <summary>
        /// <see cref="GameManager"/>インスタンス
        /// </summary>
        private GameManager _GameManager = new GameManager(new Position(OwnMachineInitialPositionX, OwnMachineInitialPositionY));

        /// <summary>
        /// <see cref="DisplayManager"/>インスタンス
        /// </summary>
        private DisplayManager _DisplayManager;

        /// <summary>
        /// <see cref="InputManager"/>インスタンス
        /// </summary>
        private InputManager _InputManager;

        #endregion


        #region プロパティ

        /// <summary>
        /// 自機の位置を設定または取得します。変更された値はVMに通知されます。
        /// </summary>
        public Position OwnMachinePosition
        {
            get
            {
                return _OwnMachinePosition;
            }
            private set
            {
                if (_OwnMachinePosition.Compare(value) == Position.Direction.Same) return;

                _OwnMachinePosition = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// フィールドサイズを取得します。
        /// </summary>
        public Position FieldSize => new Position(FieldSizeX, FieldSizeY);

        #endregion


        #region コマンド
        #endregion


        #region デリゲート
        #endregion


        #region イベント
        #endregion


        #region イベントハンドラ

        /// <summary>
        /// 機体位置変更イベントのハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _DisplayManager_MachinePositinChenged(object sender, STG.MachinePosition.MachinePositionChangedEventArgs e)
        {
            UpdateMachinePosition(sender, e);
        }

        #endregion


        #region メソッド

        /// <summary>
        /// 各管理インスタンスを生成します。
        /// </summary>
        private void InitInstances()
        {
            _GameManager.SetFieldSize(new Position(FieldSizeX, FieldSizeY));

            var machineManager = _GameManager.MachineManager;

            _DisplayManager = new DisplayManager(machineManager);
            _DisplayManager.MachinePositinChenged += _DisplayManager_MachinePositinChenged;

            _InputManager = new InputManager(_GameManager.InputManager);
        }

        /// <summary>
        /// 機体位置を更新します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateMachinePosition(object sender, MachinePositionChangedEventArgs e)
        {
            OwnMachinePosition = e.Position;
        }

        /// <summary>
        /// 自機の左移動命令を登録します。
        /// </summary>
        internal void AddLeftOrder()
        {
            _InputManager.AddLeftOrder();
        }

        /// <summary>
        /// 自機の左移動命令を削除します。
        /// </summary>
        internal void RemoveLeftOrder()
        {
            _InputManager.RemoveLeftOrder();
        }


        /// <summary>
        /// 自機の右移動命令を登録します。
        /// </summary>
        internal void AddRightOrder()
        {
            _InputManager.AddRightOrder();
        }

        /// <summary>
        /// 自機の右移動命令を削除します。
        /// </summary>
        internal void RemoveRightOrder()
        {
            _InputManager.RemoveRightOrder();
        }

        /// <summary>
        /// 自機の上移動命令を登録します。
        /// </summary>
        internal void AddUpOrder()
        {
            _InputManager.AddUpOrder();
        }

        /// <summary>
        /// 自機の上移動命令を削除します。
        /// </summary>
        internal void RemoveUpOrder()
        {
            _InputManager.RemoveUpOrder();
        }

        /// <summary>
        /// 自機の下移動命令を登録します。
        /// </summary>
        internal void AddDownOrder()
        {
            _InputManager.AddDownOrder();
        }

        /// <summary>
        /// 自機の下移動命令を削除します。
        /// </summary>
        internal void RemoveDownOrder()
        {
            _InputManager.RemoveDownOrder();
        }

        /// <summary>
        /// ゲームを開始します。
        /// </summary>
        internal void StartGame()
        {
            _GameManager.StartGame();
        }

        #endregion


        #region 内部クラス
        #endregion
    }
}
