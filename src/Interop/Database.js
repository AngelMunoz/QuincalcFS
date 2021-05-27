import { Expense } from '../Types.fs.js';
import { parse as parseDTOffset } from '../.fable/fable-library.3.1.16/DateOffset.js';
import { parse as parseDecimal } from '../.fable/fable-library.3.1.16/Decimal';
import Pouchdb from 'pouchdb-browser';
import PouchdbFind from 'pouchdb-find';

Pouchdb.plugin(PouchdbFind);

const expenses = new Pouchdb('expenses');
setupIndexes();

/**
 * 
 * @param {Partial<Expense>} expense 
 */
function mapExpense(expense) {
  return new Expense(expense._id, expense.rev, expense.name, parseDTOffset(expense.dueTo), parseDecimal(expense.amount));
}

/**
 * 
 * @param {{_id: string; rev?:string; name: string; dueTo: Date; amount: string }} expense 
 * @returns 
 */
export async function saveExpense(expense) {
  try {
    const result = await expenses.put({ ...expense, amount: expense.amount.toString() });
    return result.ok;
  } catch (error) {
    return Promise.reject(error);
  }
}

/**
 * 
 * @param {Date?} minDate 
 * @param {Date?} maxDate 
 */
export async function getExpensesByDateAndAmount(minDate, maxDate) {
  const selector = {
    ...(minDate && { dueTo: { $gt: minDate } }),
    ...(maxDate && { dueTo: { $lt: maxDate } }),
  };
  try {
    const { docs } = await expenses.find({ selector: selector, use_index: 'dueDateAndAmount', sort: [{ dueTo: 'desc', amount: 'desc' }] });
    return docs.map(mapExpense);
  } catch (error) {
    return Promise.reject(error);
  }
}


async function setupIndexes() {
  const settled = await Promise.allSettled([
    expenses.createIndex({
      index: {
        fields: ['dueTo'],
        ddoc: 'findDueTo'
      }
    }),
    expenses.createIndex({
      index: {
        fields: ['name'],
        ddoc: 'findName'
      }
    }),
    expenses.createIndex({
      index: {
        fields: ['amount'],
        ddoc: 'findAmount'
      }
    }),
    expenses.createIndex({
      index: {
        fields: ['dueTo', 'amount'],
        ddoc: 'dueDateAndAmount'
      }
    })
  ]);

  for (const status of settled) {
    if (status.status === 'rejected') {
      console.warn(`Index Creation Rejected: ${status.reason}`);
    }
  }
}