
    const keyboard = document.getElementById('keyboard');
    const input = document.getElementById('nome');
    const keys = [
      ..."ABCDEFGHIJ",
      ..."KLMNOPQRST",
      ..."UVWXYZ",
      "←", "Espaço", "Resetar"
    ];

    keys.forEach(key => {
      const button = document.createElement('button');
      button.textContent = key;
      button.classList.add('key');

      if (key === "←") button.classList.add('key-wide');
      if (key === "Espaço") button.classList.add('key-wide');
      if (key === "Resetar") button.classList.add('key-wide');

      button.addEventListener('click', () => {
        if (key === "←") {
          input.value = input.value.slice(0, -1);
        } else if (key === "Espaço") {
          input.value += " ";
        } else if (key === "Resetar") {
          input.value = "";
        } else {
          input.value += key;
        }
      });

      keyboard.appendChild(button);
    });

    document.getElementById('continuar').addEventListener('click', () => {
      alert(`Nome inserido: ${input.value}`);
      //  redirecionar para a próxima página
      window.location.href = "cpf.html"; 
    });