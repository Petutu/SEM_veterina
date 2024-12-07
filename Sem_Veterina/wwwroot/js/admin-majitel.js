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
        document.getElementById('detail-username').value = data.username || 'Není zadané'; 
        document.getElementById('detail-name').value = data.jméno_majitele || ''; 
        document.getElementById('detail-lastname').value = data.příjmení_majitele
 || ''; 
        document.getElementById('detail-phone').value = data.telefonní_číslo
 || ''; 
        document.getElementById('detail-email').value = data.email_majitele || ''; 
        document.getElementById('detail-address').value = data.adresa_majitele || ''; 
        document.getElementById('detail-klinikname').value = data.název_kliniky || ''; 
        document.getElementById('detail-addressklinik').value = data.
adresa_kliniky || ''; 
        document.getElementById('detail-animalname').value = data.jméno_zvířete
 || ''; 
        document.getElementById('detail-kind').value = data.druh_zvířete
 || ''; 
        document.getElementById('detail-health').value = data.zdravotní_stav
 || ''; 
    };