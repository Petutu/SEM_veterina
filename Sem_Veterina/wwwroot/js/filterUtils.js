export function resetFormAndSubmit() {
    const form = document.querySelector('.filter');
    form.querySelectorAll('input[type="text"]').forEach(input => input.value = '');
    form.submit();
}