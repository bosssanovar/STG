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
    /// Key Downコマンドを提供するビヘイビア
    /// </summary>
    public class KeyDownBehavior : Behavior<UIElement>
    {
        /// <summary>
        /// <see cref="KeyDownCommandProperty"/>を設定または取得します。
        /// </summary>
        public ICommand KeyDownCommand
        {
            get { return (ICommand)GetValue(KeyDownCommandProperty); }
            set { SetValue(KeyDownCommandProperty, value); }
        }

        /// <summary>
        /// Key Downコマンドの依存プロパティ
        /// </summary>
        public static readonly DependencyProperty KeyDownCommandProperty =
            DependencyProperty.Register("KeyDownCommand", typeof(ICommand), typeof(KeyDownBehavior), new UIPropertyMetadata(null));


        /// <summary>
        /// イベントを登録します。
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += new KeyEventHandler(AssociatedObjectKeyDown);
            base.OnAttached();
        }

        /// <summary>
        /// イベントを解放します。
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= new KeyEventHandler(AssociatedObjectKeyDown);
            base.OnDetaching();
        }

        private void AssociatedObjectKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDownCommand != null)
            {
                KeyDownCommand.Execute(e.Key);
            }
        }
    }
}
