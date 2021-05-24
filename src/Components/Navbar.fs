module Components.Navbar

open Types
open Sutil
open Sutil.Bulma
open Sutil.DOM
open Sutil.Attr
open Stores

let private getTheme (isDarkTheme: bool) =
  match isDarkTheme with
  | true -> text "🌞"
  | false -> text "🌚"

let view (navigateTo: Option<Page -> unit>) =
  let navOpen = Store.make false

  let onMenuItemClick (page: Page) =
    Option.iter (fun fn -> fn page) navigateTo

  let isDark = Settings.IsDarkThemeActive
  let isLight = Settings.IsLightThemeActive

  bulma.navbar [
    disposeOnUnmount [ navOpen ]
    navbar.isFixedBottom
    bindClass isDark "is-dark"
    bindClass isLight "is-light"
    bulma.navbarBrand.div [
      bulma.navbarItem.a [
        text "Sutil Experiments"
        onClick (fun _ -> onMenuItemClick Home) []
      ]
      bulma.navbarBurger [
        bindClass navOpen "is-active"
        onClick (fun _ -> Store.set navOpen (Store.get navOpen |> not)) []
        Html.span [ Attr.ariaHidden true ]
        Html.span [ Attr.ariaHidden true ]
        Html.span [ Attr.ariaHidden true ]
      ]
    ]
    bulma.navbarMenu [
      bindClass navOpen "is-active"
      bulma.navbarStart.div [
        bulma.navbarItem.a [
          bindFragment Settings.IsDarkThemeActive getTheme
          onClick (fun _ -> Settings.SwitchTheme()) []
        ]
      ]
      bulma.navbarEnd.div [
        bulma.navbarItem.a [
          text "Home"
          onClick (fun _ -> onMenuItemClick Home) []
        ]
        bulma.navbarItem.a [
          text "About"
          onClick (fun _ -> onMenuItemClick Settings) []
        ]
      ]
    ]
  ]
