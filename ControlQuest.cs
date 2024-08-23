using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlQuest : MonoBehaviour
{
    [System.Serializable]
    public class Soals
    {
        [System.Serializable]
        public class ElementSoals //nomor 1 dan seterusnya
        {
            public Sprite spritesoal; //soal image
            public string stringsoal; //soal string atau text
            public Sprite[] spriteJawabans; //jawaban image
            public string[] stringJawabans; //jawaban text
            public int kunciJawaban; //kunci jawaban
        }
        public ElementSoals elementSoals;
    }
    public Soals[] soals;

    [Header("Random Index")]
    //Random Index
    public int[] indexRandomSoal;
    public int[] indexRandomJawaban;
    public int totalSoal; //total soal yang akan dipakai
    public int urutanSoal; //0-1
    int jawabanBenar;

    [Header("UI soal dan jawaban")]
    public Image imageSoal; //soal image
    public Text textSOal; //soal text atau string
    public Image[] imageJawabans; //jawaban dengan image
    public Text[] textJawabans; //jawaban dengan text

    [Header("kondisi next soal")]
    public bool isJawabanHarusBenar; //setting
    [Tooltip("hanya untuk debug")]
    public bool isJawabanBenar; //hanya untuk dilihat

    [Header("update level")]
    public int totalScore;

    [Header("score")]
    //public Text textScore; //skor di in game
    public Text textScoreAkhir; //skor di panel end game
    public int increaseScore; //skor yang ditambahkan
    public static int totalScoreAkhir;
    public GameObject panelEndGame;

    [Header("tombol coba lagi")]
    public Button tryAgainButton;

    [Header("tombol Main Menu")]
    public Button mainMenuButton;

    [Header("tombol Kembali")]
    public Button kembaliButton;

    [Header("Teks pesan di endgame")]
    public Text messageText; // Tambahkan reference untuk teks pesan

    [Header("teks total jawaban benar dan salah")]
    public Text totaljawabanbenardansalah;
    public int totalJawabanBenar = 0;
    public int totalJawabanSalah = 0;

    [Header("audio")]
    public AudioSource audioSource; // Referensi AudioSource

    public AudioClip audioBenar; // Audio untuk jawaban benar
    public AudioClip audioSalah; // Audio untuk jawaban salah

    [Header("Panel benar salah")]
    public GameObject panelBenar; // Panel untuk jawaban benar
    public GameObject panelSalah; // Panel untuk jawaban salah

    [Header("audio")]
    public AudioSource audioNetnot; // Referensi AudioSource untuk audio netnot
    public AudioSource audioTengTeng; // Referensi AudioSource untuk audio teng teng

    private bool isPanelTertutup = true; // Flag untuk menandai apakah panel benar atau salah sedang ditampilkan

    [Header("Warna Jawaban")]
    public Color warnaJawabanBenar = Color.green;
    public Color warnaJawabanSalah = Color.red;

    public GameObject tombolNext; // Tombol untuk lanjut ke soal berikutnya

    // Dictionary untuk menyimpan jumlah kesalahan setiap soal
    private Dictionary<int, int> soalMistakes = new Dictionary<int, int>();

    void Start()
    {
        // Inisialisasi jumlah kesalahan untuk setiap soal
        for (int i = 0; i < soals.Length; i++)
        {
            soalMistakes[i] = 0;
        }

        GenerateIndexRandomSoal();
        GenerateIndexRandomJawaban();
        GenerateSoal();
    }

    // Update dipanggil sekali per frame
    void Update()
    {

    }

    void IncreaseScore()
    {
        totalScoreAkhir += increaseScore; //menambah skor
                                          //textScore.text = totalScoreAkhir.ToString(); //update ui score di in game
    }

    void UpdateLevel()
    {
        // Selalu periksa kondisi skor dan perbarui pesan teks serta memutar audio yang sesuai
        if (totalScoreAkhir >= 100)
        {
            messageText.text = "Wah skor kamu sempurna! Mantap!";
            audioTengTeng.Play();
            tryAgainButton.interactable = false;
            mainMenuButton.interactable = true;
        }
        else if (totalScoreAkhir >= 80)
        {
            messageText.text = "Wah kamu hebat!";
            audioTengTeng.Play();
            tryAgainButton.interactable = false;
            mainMenuButton.interactable = true;
        }
        else if (totalScoreAkhir < 60)
        {
            messageText.text = "Nilai kamu masih rendah nih, coba lagi yuk!";
            audioNetnot.Play();
            tryAgainButton.interactable = true;
            mainMenuButton.interactable = true;
        }
        else
        {
            // Mereset skor jika nilai di bawah 80
            totalScoreAkhir = 0;
            tryAgainButton.interactable = true;
            mainMenuButton.interactable = true;
            messageText.text = "Nilai kamu masih rendah nih, coba lagi yuk!";
        }

        // Perbarui level hanya jika skor lebih dari 80 dan level saat ini belum ditingkatkan
        if (MainMenu.levelSekarang > MainMenu.currentLevel - 1 && totalScoreAkhir >= 80)
        {
            MainMenu.currentLevel += 1;
            PlayerPrefs.SetInt("Level", MainMenu.currentLevel);
            Debug.Log("Script masuk");
        }
    }


    IEnumerator TutupPanel(GameObject panel)
    {
        yield return new WaitForSeconds(1.5f); // Tunggu selama 2 detik
        panel.SetActive(false); // Tutup panel setelah 2 detik
    }

    public void ButtonJawabans(int indexJawaban)
    {
        if (indexRandomJawaban[indexJawaban] == jawabanBenar)
        {
            Debug.Log("benar");

            isJawabanBenar = true;
            IncreaseScore();
            totalJawabanBenar++;
            panelBenar.SetActive(true);
            panelSalah.SetActive(false);
            audioSource.PlayOneShot(audioBenar);

            // Ganti warna tombol jawaban menjadi hijau untuk jawaban benar
            imageJawabans[indexJawaban].color = warnaJawabanBenar;
            StartCoroutine(TutupPanel(panelBenar));

        }
        else
        {
            Debug.Log("salah");
            totalJawabanSalah++;
            panelSalah.SetActive(true);
            panelBenar.SetActive(false);
            audioSource.PlayOneShot(audioSalah);

            // Ganti warna tombol jawaban menjadi merah untuk jawaban salah
            imageJawabans[indexJawaban].color = warnaJawabanSalah;
            StartCoroutine(TutupPanel(panelSalah));

            // Tambahkan kesalahan pada soal yang sedang dijawab
            soalMistakes[indexRandomSoal[urutanSoal]]++;
        }

        tombolNext.SetActive(true); // Aktifkan tombol next setelah menjawab
    }

    public void TombolLanjutKetiga()
    {
        // Reset nilai totalScoreAkhir
        totalScoreAkhir = 0;

        // Atur interactable tombol
        tryAgainButton.interactable = true; // Jadikan tombol coba lagi dapat berinteraksi
        mainMenuButton.interactable = true; // Jadikan tombol menu dapat berinteraksi


        // Panggil fungsi-fungsi lain yang diperlukan untuk mengatur ulang permainan
        GenerateIndexRandomSoal();
        GenerateIndexRandomJawaban();
        GenerateSoal();
    }

    public void TombolMainMenu()
    {
        // Reset nilai totalScoreAkhir
        totalScoreAkhir = 0;
        totalScore = 0; // Reset total score to 0
        totalJawabanBenar = 0; // Reset total jawaban benar
        totalJawabanSalah = 0; // Reset total jawaban salah

        // Panggil fungsi-fungsi lain yang diperlukan untuk mengatur ulang permainan
        GenerateIndexRandomSoal();
        GenerateIndexRandomJawaban();
        GenerateSoal();

        

        // Tutup panel end game jika terbuka
        panelEndGame.SetActive(false);

        // Tambahkan log untuk memastikan fungsi dipanggil
        Debug.Log("Permainan diatur ulang dan skor di-reset menjadi 0.");
    }

    public void TombolCobaLagi()
    {
        // Reset nilai totalScoreAkhir
        totalScoreAkhir = 0;

        // Panggil fungsi-fungsi lain yang diperlukan untuk mengatur ulang permainan
        GenerateIndexRandomSoal();
        GenerateIndexRandomJawaban();
        GenerateSoal();
    }

    public void TombolKembali()
    {
        // Reset nilai totalScoreAkhir
        totalScoreAkhir = 0;

        // Panggil fungsi-fungsi lain yang diperlukan untuk mengatur ulang permainan
        GenerateIndexRandomSoal();
        GenerateIndexRandomJawaban();
        GenerateSoal();
    }

    void GenerateNextSoal() //generate next soal setelah menjawab
    {
        if (urutanSoal < totalSoal - 1 && isPanelTertutup) // Periksa apakah panel tertutup sebelum melanjutkan ke soal berikutnya
        {
            TampilkanSoalBerikutnya();
        }
        else
        {
            Debug.Log("finish game");
            //panel end game
            panelEndGame.SetActive(true);
            textScoreAkhir.text = totalScoreAkhir.ToString(); //update text ui di panel end game
            totaljawabanbenardansalah.text = "Jawaban Benar: " + totalJawabanBenar + "\nJawaban Salah: " + totalJawabanSalah; // Tampilkan total jawaban benar dan salah
                                                                                                                              //finish
                                                                                                                              //star rating
            UpdateLevel();
        }
        panelBenar.SetActive(false); // Menutup panel benar
        panelSalah.SetActive(false); // Menutup panel salah
        tombolNext.SetActive(false); // Memunculkan tombol next
        GenerateSoal(); // Memanggil GenerateSoal untuk melanjutkan ke soal berikutnya
    }

    void TampilkanSoalBerikutnya()
    {
        urutanSoal += 1; // Menambah urutan soal / lanjut ke soal berikutnya
        GenerateIndexRandomJawaban(); // Mengacak posisi jawaban lagi
        GenerateSoal();
        isJawabanBenar = false; // Mengembalikan kondisi ini
    }

    void GenerateIndexRandomJawaban()
    {
        indexRandomJawaban = new int[4]; //kenapa 4? karena menggunakan abcd, jika abc maka diisi 3
        for (int i = 0; i < indexRandomJawaban.Length; i++)//fill slot array
        {
            indexRandomJawaban[i] = i;
        }

        for (int i = 0; i < indexRandomJawaban.Length; i++)
        {
            int a = indexRandomJawaban[i];
            int b = Random.Range(0, indexRandomJawaban.Length);
            indexRandomJawaban[i] = indexRandomJawaban[b];
            indexRandomJawaban[b] = a;
        }
    }

    void GenerateIndexRandomSoal()
    {
        List<int> soalList = new List<int>(soalMistakes.Keys);
        soalList.Sort((a, b) => soalMistakes[b].CompareTo(soalMistakes[a])); // Urutkan soal berdasarkan jumlah kesalahan, dari yang paling banyak ke yang paling sedikit

        indexRandomSoal = new int[soalList.Count]; //create slot array
        for (int i = 0; i < soalList.Count; i++)//fill slot array dengan int
        {
            indexRandomSoal[i] = soalList[i];
        }

        // Acak urutan soal
        for (int i = 0; i < indexRandomSoal.Length; i++)//random index
        {
            int a = indexRandomSoal[i];
            int b = Random.Range(0, indexRandomSoal.Length);
            indexRandomSoal[i] = indexRandomSoal[b];
            indexRandomSoal[b] = a;
        }
    }

    void GenerateSoal()
    {
        // Reset warna tombol jawaban menjadi warna default
        ResetWarnaTombolJawaban();
        // Update soal
        imageSoal.sprite = soals[indexRandomSoal[urutanSoal]].elementSoals.spritesoal;
        textSOal.text = soals[indexRandomSoal[urutanSoal]].elementSoals.stringsoal;
        // Update jawaban
        for (int i = 0; i < 4; i++)
        {
            imageJawabans[i].sprite = soals[indexRandomSoal[urutanSoal]].elementSoals.spriteJawabans[indexRandomJawaban[i]];
            textJawabans[i].text = soals[indexRandomSoal[urutanSoal]].elementSoals.stringJawabans[indexRandomJawaban[i]];
        }
        // Jawaban benar
        jawabanBenar = soals[indexRandomSoal[urutanSoal]].elementSoals.kunciJawaban;
    }

    void ResetWarnaTombolJawaban()
    {
        // Reset warna tombol jawaban menjadi warna default
        for (int i = 0; i < imageJawabans.Length; i++)
        {
            imageJawabans[i].color = Color.white; // Mengembalikan warna tombol ke warna default (putih)
        }
    }

    public void TombolNextDitekan()
    {
        panelBenar.SetActive(false); // Menutup panel benar
        panelSalah.SetActive(false); // Menutup panel salah
        tombolNext.SetActive(false); // Menyembunyikan tombol next
        GenerateNextSoal(); // Memanggil GenerateNextSoal untuk melanjutkan ke soal berikutnya
    }
}