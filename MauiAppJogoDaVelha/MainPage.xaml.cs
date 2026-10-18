
namespace MauiAppJogoDaVelha;

public partial class MainPage : ContentPage
{
    string vez = "X";
    bool jogoTerminado = false;

    public MainPage()
    {
        InitializeComponent();
    }

    // Executa quando o jogador clica em uma casa
    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Impede jogadas depois do fim da partida
        if (jogoTerminado)
            return;

        Button btn = (Button)sender;

        // Impede marcar uma casa já utilizada
        if (!btn.IsEnabled)
            return;

        // Marca a casa com X ou O
        string jogador = vez;
        btn.Text = jogador;
        btn.IsEnabled = false;

        // Verifica se o jogador ganhou
        if (VerificarVitoria(jogador))
        {
            jogoTerminado = true;

            await DisplayAlert(
                "Parabéns!",
                "O jogador " + jogador + " ganhou!",
                "OK"
            );

            return;
        }

        // Verifica se houve empate
        if (TodasAsCasasPreenchidas())
        {
            jogoTerminado = true;

            await DisplayAlert(
                "Fim de jogo!",
                "Deu velha! A partida terminou empatada.",
                "OK"
            );

            return;
        }

        // Alterna entre X e O
        if (vez == "X")
        {
            vez = "O";
        }
        else
        {
            vez = "X";
        }
    }

    // Verifica as combinações vencedoras
    private bool VerificarVitoria(string jogador)
    {
        // Primeira linha
        if (btn10.Text == jogador &&
            btn11.Text == jogador &&
            btn12.Text == jogador)
            return MarcarVitoria(btn10, btn11, btn12);

        // Segunda linha
        if (btn20.Text == jogador &&
            btn21.Text == jogador &&
            btn22.Text == jogador)
            return MarcarVitoria(btn20, btn21, btn22);

        // Terceira linha
        if (btn30.Text == jogador &&
            btn31.Text == jogador &&
            btn32.Text == jogador)
            return MarcarVitoria(btn30, btn31, btn32);

        // Primeira coluna
        if (btn10.Text == jogador &&
            btn20.Text == jogador &&
            btn30.Text == jogador)
            return MarcarVitoria(btn10, btn20, btn30);

        // Segunda coluna
        if (btn11.Text == jogador &&
            btn21.Text == jogador &&
            btn31.Text == jogador)
            return MarcarVitoria(btn11, btn21, btn31);

        // Terceira coluna
        if (btn12.Text == jogador &&
            btn22.Text == jogador &&
            btn32.Text == jogador)
            return MarcarVitoria(btn12, btn22, btn32);

        // Diagonal principal
        if (btn10.Text == jogador &&
            btn21.Text == jogador &&
            btn32.Text == jogador)
            return MarcarVitoria(btn10, btn21, btn32);

        // Diagonal secundária
        if (btn12.Text == jogador &&
            btn21.Text == jogador &&
            btn30.Text == jogador)
            return MarcarVitoria(btn12, btn21, btn30);

        return false;
    }

    // Pinta de rosa a combinação vencedora
    private bool MarcarVitoria(Button b1, Button b2, Button b3)
    {
        Color rosa = Color.FromArgb("#FF69B4");

        b1.BackgroundColor = rosa;
        b2.BackgroundColor = rosa;
        b3.BackgroundColor = rosa;

        // Bloqueia todas as casas até reiniciar
        DesativarBotoes();

        return true;
    }

    // Verifica se todas as casas foram preenchidas
    private bool TodasAsCasasPreenchidas()
    {
        return !btn10.IsEnabled &&
               !btn11.IsEnabled &&
               !btn12.IsEnabled &&
               !btn20.IsEnabled &&
               !btn21.IsEnabled &&
               !btn22.IsEnabled &&
               !btn30.IsEnabled &&
               !btn31.IsEnabled &&
               !btn32.IsEnabled;
    }

    // Desativa as nove casas
    private void DesativarBotoes()
    {
        btn10.IsEnabled = false;
        btn11.IsEnabled = false;
        btn12.IsEnabled = false;

        btn20.IsEnabled = false;
        btn21.IsEnabled = false;
        btn22.IsEnabled = false;

        btn30.IsEnabled = false;
        btn31.IsEnabled = false;
        btn32.IsEnabled = false;
    }

    // Reinicia a partida
    private void Reiniciar_Clicked(object sender, EventArgs e)
    {
        Zerar();
    }

    // Limpa as casas e restaura as cores
    private void Zerar()
    {
        Button[] botoes =
        {
            btn10, btn11, btn12,
            btn20, btn21, btn22,
            btn30, btn31, btn32
        };

        foreach (Button botao in botoes)
        {
            botao.Text = "";
            botao.IsEnabled = true;
            botao.BackgroundColor = Color.FromArgb("#808080");
        }

        vez = "X";
        jogoTerminado = false;
    }
}
