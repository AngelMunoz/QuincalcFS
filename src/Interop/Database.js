import { parse as parseLong } from '../.fable/fable-library.3.1.16/Long.js';
import { parse as parseDecimal } from '../.fable/fable-library.3.1.16/Decimal.js';
import { Expense } from '../Types.fs.js';

export function saveExpense(expense) {
    console.log(expense);
    const exp = JSON.parse(JSON.stringify(expense));
    console.log(exp);
    exp.dueTo = parseLong(exp.dueTo);
    exp.amount = parseDecimal(exp.amount);
    const exp2 = new Expense(exp._id, exp.name, exp.dueTo, exp.amount);
    console.log(expense, exp, exp2.Equals(expense));
}