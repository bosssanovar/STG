using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace STGApp.Views.Behavior
{
    /// <summary>
    /// Key Upコマンドを提供するビヘイビア
    /// </summary>
    public class KeyUpBehavior : Behavior<UIElement>
    {
        /// <summary>
        /// <see cref="KeyUpCommandProperty"/>を設定または取得します。
        /// </summary>
        public ICommand KeyUpCommand
        {
            get { return (ICommand)GetValue(KeyUpCommandProperty); }
            set { SetValue(KeyUpCommandProperty, value); }
        }

        /// <summary>
        /// Key Upコマンドの依存プロパティ
        /// </summary>
        public static readonly DependencyProperty KeyUpCommandProperty =
            DependencyProperty.Register("KeyUpCommand", typeof(ICommand), typeof(KeyUpBehavior), new UIPropertyMetadata(null));


        /// <summary>
        /// イベントを登録します。
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.KeyUp += new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnAttached();
        }

        /// <summary>
        /// イベントを解放します。
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.KeyUp -= new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnDetaching();
        }

        private void AssociatedObjectKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUpCommand != null)
            {
                KeyUpCommand.Execute(e.Key);
            }
        }
    }
}
