// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Controls;

namespace Wpf.Ui.Extensions;

/// <summary>
/// Extensions for the <see cref="INavigationService"/>.
/// </summary>
public static class NavigationServiceExtensions
{
    /// <summary>
    /// 菜单替换后清理导航状态，并等待内容框架提交空页面。
    /// </summary>
    /// <param name="navigationService">使用该导航控件的服务。</param>
    /// <returns>完成导航清理的任务。</returns>
    public static Task ResetNavigationAsync(this INavigationService navigationService)
    {
        INavigationView? control = navigationService is NavigationService service
            ? service.NavigationControl
            : navigationService.GetNavigationControl();

        if (control is NavigationView navigationView)
        {
            return navigationView.ResetNavigationAsync();
        }

        control?.CleanCache();
        control?.ClearJournal();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Sets the pane display mode of the navigation service.
    /// </summary>
    /// <param name="navigationService">The navigation service.</param>
    /// <param name="paneDisplayMode">The pane display mode.</param>
    /// <returns>Same <see cref="INavigationService"/> so multiple calls can be chained.</returns>
    public static INavigationService SetPaneDisplayMode(
        this INavigationService navigationService,
        NavigationViewPaneDisplayMode paneDisplayMode
    )
    {
        INavigationView? navigationControl = navigationService.GetNavigationControl();

        if (navigationControl is not null)
        {
            navigationControl.PaneDisplayMode = paneDisplayMode;
        }

        return navigationService;
    }
}
