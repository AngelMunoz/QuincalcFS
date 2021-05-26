module Types

type Page =
  | Home
  | Expenses
  | Payments
  | Settings

type Theme =
  | Light
  | Dark

type Expense =
  { _id: string
    name: string
    dueTo: int64
    amount: decimal }

type AppSettings = { theme: Theme option }

type AppResource =
  | Expense
  | Payment

[<RequireQualifiedAccess>]
type AppCommand =
  | New of AppResource
  | Open of AppResource
