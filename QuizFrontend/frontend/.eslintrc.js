module.exports = {
    env: {
      browser: true,
      es2021: true,
    },
    extends: [
      'plugin:vue/vue3-essential', // Ensure Vue 3 rules are applied
      'eslint:recommended',
    ],
    parserOptions: {
      ecmaVersion: 2021,
      sourceType: 'module',
    },
    rules: {
      'no-undef': 'off', // Turn off the undefined variable rule
    },
  };
  