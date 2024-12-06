const convertKeysToLowerCase = (data) => {
        const newData = {};
        for (let key in data) {
            if (data.hasOwnProperty(key)) {
                newData[key.toLowerCase()] = data[key];
            }
        }
        return newData;
    };

    const fetchMajitelDetails = async (id) => {
        try {
            const response = await fetch(`http://localhost:5240/api/Majitel/${id}`);
            if (!response.ok) {
                console.error('Chyba při načítání dat kliniky:', response.statusText);
                return;
            }
            const data = await response.json();
            const lowerCaseData = convertKeysToLowerCase(data);  // Převod klíčů na malá písmena
            console.log(lowerCaseData);  // Zkontrolujte, co API vrací
            updateDetailPanel(lowerCaseData);
        } catch (error) {
            console.error('Chyba při volání API:', error);
        }
    };

    const tableRows = document.querySelectorAll('.table-list-body-row');

    tableRows.forEach(row => {
        row.addEventListener('click', () => {
            const id = row.dataset.id; // Pokud je id kliniky uloženo v datatributu
            fetchMajitelDetails(id); // Zavolání API pro načtení detailu kliniky
        });
    });

    const updateDetailPanel = (data) => {
        console.log(data.název);  // Opravený název
        document.getElementById('detail-name').value = data.název || ''; 

        console.log(data.adresa);  // Opravená adresa
        document.getElementById('detail-address').value = data.adresa || ''; 

        console.log(data.telefonní_číslo);  // Opravené telefonní číslo
        document.getElementById('detail-phone').value = data.telefonní_číslo || ''; 

        console.log(data.email);  // Opravený email
        document.getElementById('detail-email').value = data.email || ''; 
    };