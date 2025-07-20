// src/env.d.ts

interface ImportMetaEnv {
  readonly BASE_URL: string;
  readonly MODE: string;
  readonly DEV: boolean;
  readonly PROD: boolean;
  // 如果您在 .env 文件中有自定义的 VUE_APP_ 开头的变量，也可以在这里声明
  // readonly VUE_APP_API_URL: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}