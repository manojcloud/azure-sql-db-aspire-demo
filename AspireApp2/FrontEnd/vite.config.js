import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

const { gitDescribeSync } = require('git-describe');

process.env.VITE_APP_VERSION = require('./package.json').version
process.env.VITE_APP_GIT_HASH = gitDescribeSync().hash

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    host: true,
    port: parseInt(process.env.PORT ?? "5173"),
    proxy: {
      '/api': {
        target: process.env.services__dab__https__0 || process.env.services__dab__http__0,
        changeOrigin: true,
        secure: false
      }
    }
  }
})
