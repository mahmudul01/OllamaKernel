## 🦙 Semantic Kernel with Ollama in .NET

This repository demonstrates how to integrate Ollama with Microsoft Semantic Kernel in a .NET console application. It connects a local LLM model (like `llama3.1`) with Semantic Kernel plugins, allowing you to interact with structured data (from JSON files) as well as general LLM capabilities.

### 🚀 Features
- **Run Ollama models locally** (no cloud needed).
- **Use Semantic Kernel plugins** to expose custom functionality.
- **Example Users plugin** that reads user data from `users.json`.
- **Example Lights plugin** (mock smart home automation).
- **Combined conversational AI**:
  - If the user asks about users or lights, the AI calls the plugin.
  - If the user asks anything else (coding, general knowledge, etc.), the LLM responds directly.

### ⚡ Getting Started
1. **Install Ollama**
   - Download and install Ollama: [https://ollama.ai/download](https://ollama.ai/download)

2. **Pull a Model**
   For this project, we’ll use `llama3.1`:
   ```bash
   ollama pull llama3.1
   ```

3. **Run the Ollama**
    - Start the server:
    ```bash
    ollama serve
    ```
  - It runs by default at: `http://localhost:11434`

3. **Run the Application**
    ```
   You’ll see a console prompt:
   ```
   Input -->
   ```

   Examples:
   - Get me all users → Calls UsersPlugin and returns the JSON users.
   - Find user with email alice.johnson@example.com → Fetches specific user.
   - Turn on the living room light → Calls LightsPlugin.
   - What is Docker? → Answered directly by the LLM.

### 📂 Project Structure
```
.
├── users.json           # Sample JSON dataset for Users plugin
├── UsersPlugin.cs       # Semantic Kernel plugin for user data
├── LightsPlugin.cs      # Semantic Kernel plugin for mock lights
├── Program.cs           # Console app entry point
└── README.md            # Documentation
```

### 🧩 Users Plugin Example
`users.json`
```json
[
  {
    "id": 1,
    "name": "Alice Johnson",
    "email": "alice.johnson@example.com",
    "phone": "+1-202-555-0101"
  },
  {
    "id": 2,
    "name": "Bob Smith",
    "email": "bob.smith@example.com",
    "phone": "+1-202-555-0102"
  }
]
```

**Plugin Functions**
- `get_users` → Returns all users.
- `get_user_by_id` → Fetch by user ID.
- `get_user_by_email` → Fetch by email.
- `get_user_by_phone` → Fetch by phone number.

### ⚠️ Notes
- Not all Ollama models support function-calling (plugins). Tested with `llama3.1`.
- You can extend this by adding more plugins (e.g., database, APIs).

### 📖 References
- [Ollama Docs](https://github.com/ollama/ollama)
- [Microsoft Semantic Kernel](https://github.com/microsoft/semantic-kernel)

### 🛠️ Future Improvements
- Add more plugins (e.g., Weather, Filesystem).
- Use embeddings for semantic search over large datasets.
- Build a web UI on top of the console app.

### 👨‍💻 Author
Developed by [Mahmudul Hasan] ✨


