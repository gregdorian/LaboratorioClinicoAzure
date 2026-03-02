# Frontend - Installation & Setup Guide

## 🚨 Important: Run npm install First!

La mayoría de los "errores" que ves en VS Code desaparecerán después de instalar las dependencias.

### Installation Steps

```bash
cd frontend
npm install
```

This will:
- ✅ Install all 55+ dependencies
- ✅ Resolve all module "Cannot find module" errors
- ✅ Setup node_modules correctly
- ✅ Generate package-lock.json

### Verify Installation

After `npm install`, run:

```bash
npm run type-check
```

This will verify TypeScript compilation without errors.

### Common Issues & Solutions

#### Issue 1: "Cannot find module 'react'"
**Solution**: Run `npm install` (this installs @types/react automatically)

#### Issue 2: Tailwind CSS @tailwind warnings in index.css
**Solution**: These are CSS linter warnings, not actual errors. They disappear when you run `npm run dev`

#### Issue 3: "Vite unknown env variable"
**Solution**: Create `.env` file from `.env.example`:
```bash
cp .env.example .env
```

#### Issue 4: ESLint complaints
**Solution**: Run ESLint to auto-fix issues:
```bash
npm run lint -- --fix
```

### Development Workflow

1. **Install dependencies**:
   ```bash
   npm install
   ```

2. **Start development server**:
   ```bash
   npm run dev
   ```
   Opens at: `http://localhost:5173`

3. **Check TypeScript**:
   ```bash
   npm run type-check
   ```

4. **Lint code**:
   ```bash
   npm run lint
   ```

5. **Build for production**:
   ```bash
   npm run build
   ```

### File Structure Check

```
frontend/
├── src/
│   ├── components/                    ✅ (23 files)
│   ├── hooks/                         ✅ (5 files)
│   ├── lib/                           ✅ (2 files)
│   ├── pages/                         ✅ (8 files)
│   ├── App.tsx                        ✅
│   ├── main.tsx                       ✅
│   ├── index.css                      ✅
│   └── vite-env.d.ts                  ✅
├── Configuration files                ✅ (7 files)
├── package.json                       ✅
├── .env.example                       ✅
├── .gitignore                         ✅
├── setup.sh                           ✅
└── README.md                          ✅
```

### Configuration Verification

All required configuration files are present:
- ✅ vite.config.ts
- ✅ tsconfig.json
- ✅ tailwind.config.ts
- ✅ postcss.config.js
- ✅ eslint.config.js
- ✅ .env.example
- ✅ vite-env.d.ts

### Environment Setup

1. Create `.env` file:
   ```bash
   cp .env.example .env
   ```

2. Update `VITE_API_BASE_URL` in `.env`:
   ```env
   VITE_API_BASE_URL=http://localhost:5000/api
   ```

### Troubleshooting

**Dev server not starting?**
```bash
# Clear node_modules and reinstall
rm -rf node_modules package-lock.json
npm install
npm run dev
```

**TypeScript errors in editor?**
- Restart VS Code
- Wait for TypeScript server to initialize (watch status bar)
- Run: `npm run type-check`

**Module resolution issues?**
```bash
# Verify vite.config.ts has correct path aliases
# Check tsconfig.json compilerOptions.paths
npm run type-check
```

### Next Steps After Installation

1. ✅ Run `npm install`
2. ✅ Create `.env` file
3. ✅ Start dev server: `npm run dev`
4. ✅ Browser opens at http://localhost:5173
5. ✅ Verify all pages load correctly (Landing, Login, Register)
6. ✅ Test theme switcher (dark/light mode)
7. ✅ Backend integration ready to begin

### No More Errors Expected After:
```bash
npm install && npm run dev
```

All TypeScript, module, and import errors will resolve automatically.
