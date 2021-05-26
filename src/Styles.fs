[<RequireQualifiedAccess>]
module Styles

open Sutil.Styling
open Sutil.DOM

let Page =
  rule ".page" [ Css.marginTop (Feliz.length.em 1) ]
