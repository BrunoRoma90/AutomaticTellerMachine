//Methods CODE 

//function insertNumber(num) {
//    var number = document.getElementById('screen').innerHTML
//    document.getElementById('screen').innerHTML = number + num;
//}

//function clearScreen() {
//    const choice = document.getElementById('screen')
//    choice.innerHTML = ""

//}

//function backspace() {
//    const choice = document.getElementById('screen')
//    choice.innerHTML = choice.innerHTML.substring(0, choice.innerHTML.length - 1)
//}

//function confirm() {
//    const choice = document.getElementById('screen')
//    choice.innerHTML = ""

//}

//function atmSubmitForm() {
//    document.getElementById('screenForm').submit()
//}
//--------------------------------------------------------------------------------------------------------------//
let currentInput = null;

// Atualiza o campo ativo ao focar em qualquer input
document.addEventListener('focusin', (event) => {
    if (event.target.tagName === 'INPUT') {
        console.log(currentInput);
        currentInput = event.target; // Atualiza o campo ativo
    }
});

//function insertNumber(num) {
//    if (currentInput) {
//        let value = currentInput.value
//    }
//    if (num === '.' && value.includes('.')) return;

//    currentInput.value = value + num
//}

function clearScreen() {
    if (currentInput) {
        currentInput.value = ''; // Limpa o valor atual
    }
}

function backspace() {
    if (currentInput) {
        currentInput.value = currentInput.value.slice(0, -1); // Remove o último caracter
    }
}
 //Insere número no campo atualmente ativo
function insertNumber(num) {
    if (currentInput) {
        if (num === '.' && currentInput.value.includes('.')) return;
        currentInput.value += num; // Adiciona o número ao valor atual do campo
    }
}

function atmSubmitForm() {
    const activeForm = document.querySelector('form');
    if (activeForm) {
        activeForm.submit();
    }

}

//function insertNumber(num) {
//    if (currentInput) {
//        // Captura o valor atual como texto
//        let value = currentInput.value;

//        // Se for um ponto, garante que apenas um único ponto seja permitido
//        if (num === '.') {
//            num = '.0'
            
//        }
//        if (value.includes('.0')) {
//            num = currentInput.value.split('.')[0] + '.' + num
//            currentInput.value = num;
//            return      
//        }

//        // Adiciona o número ou ponto ao valor atual
//        value += num;

//        // Atualiza o campo com o novo valor
//        currentInput.value = value;
//    }
//}

