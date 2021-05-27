module Types

open System

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
    rev: string option
    name: string
    dueTo: DateTimeOffset
    amount: float }

type AppSettings = { theme: Theme option }

type AppResource =
  | Expense
  | Payment

[<RequireQualifiedAccess>]
type AppCommand =
  | New of AppResource
  | Open of AppResource
