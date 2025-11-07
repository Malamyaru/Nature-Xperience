using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class XRRayFactDisplay : MonoBehaviour
{
    [Header("XR Ray Interactors")]
    public XRRayInteractor leftRayInteractor;
    public XRRayInteractor rightRayInteractor;

    [Header("English UI References")]
    public TMP_Text factTextEN;
    public Image factImageEN;

    [Header("Japanese UI References")]
    public TMP_Text factTextJP;
    public Image factImageJP;

    void Update()
    {
        if (factTextEN == null || factImageEN == null || factTextJP == null || factImageJP == null)
            return;

        bool hitFound = false;
        hitFound |= CheckRayHit(leftRayInteractor);
        hitFound |= CheckRayHit(rightRayInteractor);

        factTextEN.gameObject.SetActive(hitFound);
        factImageEN.gameObject.SetActive(hitFound);
        factTextJP.gameObject.SetActive(hitFound);
        factImageJP.gameObject.SetActive(hitFound);

        if (!hitFound)
        {
            factTextEN.text = "";
            factTextJP.text = "";
        }
    }

    private bool CheckRayHit(XRRayInteractor interactor)
    {
        if (interactor == null)
            return false;

        if (interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            switch (hit.collider.tag)
            {
                case "Body":
                    factTextEN.text = "Kabutomushi can lift up to 850x their own body weight!";
                    factTextJP.text = "カブトムシは自分の体重の850倍まで持ち上げることができます！";
                    return true;
                case "Horn":
                    factTextEN.text = "Males use their forked horns to battle rivals for mates.";
                    factTextJP.text = "オスは枝分かれした角を使って、交尾相手を巡り他のオスと戦います。";
                    return true;
                case "Leg":
                    factTextEN.text = "Their legs help them dig into soil for protection.";
                    factTextJP.text = "脚は土に潜って身を守るのに役立ちます。";
                    return true;
                case "Larvaebody":
                    factTextEN.text = "As larvae, their bodies are soft and white, leaving them vulnerable to predators.";
                    factTextJP.text = "幼虫の体は柔らかく白いため、捕食者に狙われやすいです。";
                    return true;
                case "Larvaehead":
                    factTextEN.text = "They can only eat decaying plant matter, such as rotting wood and leaves.";
                    factTextJP.text = "腐った木や落ち葉などの植物の腐敗物しか食べることができません。";
                    return true;
                case "Pupaebody":
                    factTextEN.text = "During the pupal stage, the larvae transforms within a hardened shell before emerging as a beetle.";
                    factTextJP.text = "さなぎの段階で、幼虫は硬い殻の中で変態し、成虫のカブトムシになります。";
                    return true;
                case "Pupaehorn":
                    factTextEN.text = "The horn begins forming during the pupal stage, gradually hardening before the beetle emerges.";
                    factTextJP.text = "角はさなぎの段階で形成され始め、成虫になる前に徐々に硬化します。";
                    return true;
                case "Frogbody":
                    factTextEN.text = "Japanese tree frog's can survive the extreme cold, up to -30 degrees celcius!";
                    factTextJP.text = "ニホンアマガエルは、氷点下30度という極寒の環境でも生き延びることができます。";
                    return true;
                case "Froghead":
                    factTextEN.text = "The head of a Japanese tree frog is small but houses large eyes for spotting prey.";
                    factTextJP.text = "ニホンアマガエルの頭は小さいですが、大きな目で獲物を見つけます。";
                    return true;
                case "Frogleg":
                    factTextEN.text = "Its strong hind legs allow the frog to jump many times its body length.";
                    factTextJP.text = "強力な後ろ足により、自分の体の何倍もの距離を跳ぶことができます。";
                    return true;
                case "Crabbody":
                    factTextEN.text = "The Sawagani's body is small but protected by a hard shell that helps it survive in rocky streams.";
                    factTextJP.text = "サワガニの体は小さいですが、岩場の川で生きるための硬い甲羅に守られています。";
                    return true;
                case "Crabclaw":
                    factTextEN.text = "It uses its claws to defend itself and to pick up small insects and plants for food.";
                    factTextJP.text = "ハサミを使って身を守ったり、小さな昆虫や植物をつまんで食べたりします。";
                    return true;
                case "Crabeye":
                    factTextEN.text = "Its eyes can move independently, helping it watch for danger in every direction.";
                    factTextJP.text = "目は別々に動かすことができ、あらゆる方向の危険を見張るのに役立ちます。";
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }
}
 