module Types

type Page =
  | Home
  | Settings

type Theme =
  | Light
  | Dark

type AppSettings = { theme: Theme }

type AppResource =
  | Expense
  | Payment

type Period = Period of string

[<RequireQualifiedAccess>]
type AppCommand =
  | New of AppResource
  | Open of Period
