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
        currentInput = event.target; // Atualiza o campo ativo
    }
});

// Insere número no campo atualmente ativo
function insertNumber(num) {
    if (currentInput) {
        currentInput.value += num; // Adiciona o número ao valor atual do campo
    }
}



function atmSubmitForm() {
    const activeForm = document.querySelector('form');
    if (activeForm) {
        activeForm.submit();
    }
}