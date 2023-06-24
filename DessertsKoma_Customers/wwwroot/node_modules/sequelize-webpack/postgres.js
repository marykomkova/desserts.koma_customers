'use strict';

const PostgresDialect = require('./lib/dialects/postgres');
const Sequelize = require('./lib/sequelize');

Sequelize.prototype.getDialectObject = function(sequelize) {
  return new PostgresDialect(sequelize);
};

module.exports = Sequelize;
