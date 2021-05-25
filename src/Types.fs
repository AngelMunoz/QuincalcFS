module Types

type Page =
  | Home
  | Expenses
  | Payments
  | Settings

type Theme =
  | Light
  | Dark

type AppSettings = { theme: Theme option }

type AppResource =
  | Expense
  | Payment

[<RequireQualifiedAccess>]
type AppCommand =
  | New of AppResource
  | Open of AppResource
