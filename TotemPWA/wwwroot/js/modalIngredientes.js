let carrinho = [];
let totalItens = 0;
let totalPedido = 0;
let produtoSelecionado = null;
let ingredientes = {};  // Armazena os ingredientes e suas quantidades

// ingredientes adicionais
const precoQueijo = 2.70;
const precoCarne = 9.60;
const precoBacon = 3.50;
const precoCebola = 1.50;
const precoAlface = 1.00;
const precoTomate = 1.00;
const precoPao = 2.00;

document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('modal-personalizar').style.display = 'none';
    
    document.querySelectorAll('.produto').forEach(produto => {
        produto.addEventListener('click', function () {
            const nome = produto.querySelector('h3').textContent;
            const preco = parseFloat(produto.querySelector('p').textContent.replace('R$ ', '').replace(',', '.'));
            const imagem = produto.querySelector('img').src;
            const descricao = produto.querySelector('p:nth-of-type(2)').textContent;

            // Atualiza o modal com as informações do produto
            document.getElementById('produto-nome').textContent = nome;
            document.getElementById('produto-preco').textContent = `R$ ${preco.toFixed(2)}`;
            document.getElementById('produto-imagem').src = imagem;

            // Preenche a lista de ingredientes no modal
            const ingredientesArray = descricao.split(', ');
            const ingredientesContainer = document.getElementById('ingredientes-container');
            ingredientesContainer.innerHTML = '';
            ingredientes = {};  // Limpa os ingredientes anteriores


            ingredientesArray.forEach(ingrediente => {
                let precoIngrediente = 0;
                let quantidadePadrao = 1; 

                // Adicionando o preço dos ingredientes
                if (ingrediente.toLowerCase().includes("queijo")) precoIngrediente = precoQueijo;
                else if (ingrediente.toLowerCase().includes("carne")) precoIngrediente = precoCarne;
                else if (ingrediente.toLowerCase().includes("bacon")) precoIngrediente = precoBacon;
                else if (ingrediente.toLowerCase().includes("cebola")) precoIngrediente = precoCebola;
                else if (ingrediente.toLowerCase().includes("alface")) precoIngrediente = precoAlface;
                else if (ingrediente.toLowerCase().includes("tomate")) precoIngrediente = precoTomate;
                else if (ingrediente.toLowerCase().includes("pão")) precoIngrediente = precoPao;

                let div = document.createElement('div');
                div.classList.add('ingrediente-item');

                //  Exibindo o nome do ingrediente junto com o preço no modal
                div.innerHTML = `<span>${ingrediente} (R$ ${precoIngrediente.toFixed(2)})</span> 
                                 <div class="ingrediente-controles">
                                    <button class="diminuir">-</button> 
                                    <span id="quantidade-${ingrediente}" class="quantidade">0</span>
                                    <button class="aumentar">+</button>
                                 </div>`;
                ingredientesContainer.appendChild(div);

                // Armazena o ingrediente com quantidade inicial 0
                ingredientes[ingrediente] = 0;
            });

            
            document.getElementById('modal-personalizar').style.display = 'flex';
        });
    });

    document.querySelector('.close').addEventListener('click', function () {
        document.getElementById('modal-personalizar').style.display = 'none';
    });

    document.getElementById('diminuir').addEventListener('click', () => {
        let quantidade = parseInt(document.getElementById('quantidade-lanche').value);
        if (quantidade > 1) {
            document.getElementById('quantidade-lanche').value = quantidade - 1;
            updateCarrinho();
        }
    });


    document.getElementById('aumentar').addEventListener('click', () => {
        let quantidade = parseInt(document.getElementById('quantidade-lanche').value);
        document.getElementById('quantidade-lanche').value = quantidade + 1;
        updateCarrinho();
    });

    // Manipulação dos ingredientes
    document.getElementById('ingredientes-container').addEventListener('click', function (e) {
        if (e.target.classList.contains('aumentar')) {
            let ingrediente = e.target.closest('.ingrediente-item').querySelector('span').textContent.split(' (')[0];
            ingredientes[ingrediente]++;
            document.getElementById(`quantidade-${ingrediente}`).textContent = ingredientes[ingrediente];
        }
        if (e.target.classList.contains('diminuir')) {
            let ingrediente = e.target.closest('.ingrediente-item').querySelector('span').textContent.split(' (')[0];
            if (ingredientes[ingrediente] > 0) {
                ingredientes[ingrediente]--;
                document.getElementById(`quantidade-${ingrediente}`).textContent = ingredientes[ingrediente];
            }
        }
    });

    // Adicionar produto ao carrinho
    document.getElementById('confirmar-personalizacao').addEventListener('click', () => {
        const quantidadeLanche = parseInt(document.getElementById('quantidade-lanche').value);
        const valorProduto = parseFloat(document.getElementById('produto-preco').textContent.replace('R$ ', '').replace(',', '.'));
        let valorTotal = valorProduto * quantidadeLanche;
        let valorIngredientes = 0; // Inclui o valor do pão

        // Soma o valor dos ingredientes adicionais ao total do pedido
        for (let ingrediente in ingredientes) {
            if (ingrediente.toLowerCase().includes("queijo")) valorIngredientes += ingredientes[ingrediente] * precoQueijo;
            else if (ingrediente.toLowerCase().includes("carne")) valorIngredientes += ingredientes[ingrediente] * precoCarne;
            else if (ingrediente.toLowerCase().includes("bacon")) valorIngredientes += ingredientes[ingrediente] * precoBacon;
            else if (ingrediente.toLowerCase().includes("cebola")) valorIngredientes += ingredientes[ingrediente] * precoCebola;
            else if (ingrediente.toLowerCase().includes("alface")) valorIngredientes += ingredientes[ingrediente] * precoAlface;
            else if (ingrediente.toLowerCase().includes("tomate")) valorIngredientes += ingredientes[ingrediente] * precoTomate;
            else if (ingrediente.toLowerCase().includes("pão")) valorIngredientes += ingredientes[ingrediente] * precoPao; 
        }

        // Atualiza o carrinho
        totalPedido += valorTotal + valorIngredientes;
        totalItens += quantidadeLanche;

        document.getElementById('total-pedido').textContent = `R$ ${totalPedido.toFixed(2)}`;
        document.getElementById('total-itens').textContent = totalItens;

        // Adiciona o produto ao carrinho
        carrinho.push({
            produto: document.getElementById('produto-nome').textContent,
            quantidade: quantidadeLanche,
            ingredientes: { ...ingredientes }
        });

        document.getElementById('modal-personalizar').style.display = 'none';
    });
});